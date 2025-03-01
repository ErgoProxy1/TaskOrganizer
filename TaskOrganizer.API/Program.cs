using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

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

// Add Firestore
builder.Services.AddSingleton(FirestoreDb.Create("taskmanager-f9cd2"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
