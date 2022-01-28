using System;
using System.Windows.Data;

namespace TextReplacer.Converter {
  public class EnumBooleanConverter : IValueConverter {

    public object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
      if (value.Equals(parameter)) {
        return true;
      }
      return false;
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
      Boolean? boolValue = value as Boolean?;
      if (boolValue.HasValue && boolValue.Value) {
        return parameter;
      }
      return null;
    }
  }
}
