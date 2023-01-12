using System.Windows.Controls;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for TemplateCreatorControl.xaml
  /// </summary>
  public partial class TemplateCreatorControl : UserControl {

    public TemplateCreatorControl() {
      InitializeComponent();

      Variables.ToolTip = @"Hier können mehrere Variablen angegeben werden,
Die Reihenfolge muss dabei gleich wie im Vorgabetext gehalten werden,
Bitte Trennzeichen beachten.";
      SplitChar.ToolTip = @"Beinhaltet das Trennzeichen, zum Trennen von Variablen und Labels";
    }
  }
}
