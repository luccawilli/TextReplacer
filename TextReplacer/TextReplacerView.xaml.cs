using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
