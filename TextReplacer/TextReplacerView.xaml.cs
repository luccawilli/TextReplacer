using System.Windows.Controls;

namespace TextReplacer {
  /// <summary>
  /// Interaction logic for TextReplacerView.xaml
  /// </summary>
  public partial class TextReplacerView : UserControl {
    public TextReplacerView() {
      InitializeComponent();

      ToInsertLabels.ToolTip = "Diese Werte werden eingefügt für das 'Ersetzungszeichen', für jeden Wert ergibt es einen eigen Template-Output";

      ReplacementCharacters.ToolTip = "Dieser Wert wird im Template ersetzt";
    }
  }
}
