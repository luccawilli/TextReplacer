using System;
using System.Windows.Data;
using System.Windows.Media;
using TextReplacer.Enum;

namespace TextReplacer.Converter {
  public class ReplacerTypeIconConverter : IValueConverter {

    public ImageSource DefaultValue { get; set; }
    public ImageSource TextReplacerValue { get; set; }
    public ImageSource TemplateCreatorValue { get; set; }
    public ImageSource RgExValue { get; set; }

    public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
      ReplacerType? v = (ReplacerType?)value;
      if (v == null) {
        return DefaultValue;
      }
      switch (v) {
        case ReplacerType.TextReplacer:
          return TextReplacerValue;
        case ReplacerType.TemplateCreator:
          return TemplateCreatorValue;
        case ReplacerType.RgEx:
          return RgExValue;
        default:
          return DefaultValue;
      }
    }

    public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
      if (value == TextReplacerValue) {
        return ReplacerType.TextReplacer;
      }
      else if (value == TemplateCreatorValue) {
        return ReplacerType.TemplateCreator;

      }
      else if (value == RgExValue) {
        return ReplacerType.RgEx;
      }
      else {
        return ReplacerType.None;
      }
    }
  }
}
