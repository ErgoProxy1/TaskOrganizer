using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskOrganizer.Desktop.Helper
{
  public class MultiBindingToArrayConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      return values.Clone();
    }

    // Not needed for our purposes
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
