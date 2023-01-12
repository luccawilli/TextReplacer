using System.Windows.Controls;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for RgExControl.xaml
  /// </summary>
  public partial class TextReplacerControl : UserControl {

    public TextReplacerControl() {
      InitializeComponent();

      ToInsertLabels.ToolTip = "Diese Werte werden eingefügt für das 'Ersetzungszeichen', für jeden Wert ergibt es einen eigen Template-Output";

      ReplacementCharacters.ToolTip = "Dieser Wert wird im Template ersetzt";
    }
  }
}
