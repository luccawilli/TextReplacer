using System;
using System.Windows.Media;

namespace TextReplacer {
  public class InfoMessage {

    /// <summary>
    /// For info messages
    /// </summary>
    public static Brush TextReplacerBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#579bd3"));

    /// <summary>
    /// For warnings
    /// </summary>
    public static Brush TextReplacerPurple = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7C00E8"));

    public String Message { get; set; }

    public Brush Brush { get; set; }

  }
}
