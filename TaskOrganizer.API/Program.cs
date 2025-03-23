using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using TaskOrganizer.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200");
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
            policy.AllowCredentials();
        }
    );
});

// Load User Secrets
builder.Configuration.AddUserSecrets<Program>();

// Retrieve the Firebase configuration JSON from User Secrets
var firebaseConfigJson = builder.Configuration.GetValue<string>("FIREBASE_CONFIG");

// Write the JSON string to a temporary file
var tempFilePath = Path.GetTempFileName();
File.WriteAllText(tempFilePath, firebaseConfigJson);

// Set the environment variable to point to the temporary file
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", tempFilePath);

// Initialize Firebase
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault(),
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/taskmanager-f9cd2";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/taskmanager-f9cd2",
            ValidateAudience = true,
            ValidAudience = "taskmanager-f9cd2",
            ValidateLifetime = true
        };
    });

builder.Services.AddDbContext<TaskOrganizerDbContext>(options => options.UseNpgsql(builder.Configuration.GetValue<string>("SQL_CONNECTION")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
