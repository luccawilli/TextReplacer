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
  /// <summary>A simple tool to replace specific strings with multiple given --> so to generate filled out templates.</summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();

      TemplateText.ToolTip = @"Hier rein kommt das Template, 
also der Text in dem gewisse Bereiche ersetzt werden sollen.
Bsp: var xxx = 123 * 90; // xxx wird ersetzt mit einem Wert aus der Liste der 'Einzufügende Labels'";

      ToInsertLabels.ToolTip = "Diese Werte werden eingefügt für das 'Ersetzungszeichen', für jeden Wert ergibt es einen eigen Template-Output";

      ReplacementCharacters.ToolTip = "Dieser Wert wird im Template ersetzt";
    }


    /// <summary>Handles the click on the start button</summary>
    /// <param name="sender">Object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void HandleStartButtonClicked(Object sender, RoutedEventArgs e) {
      String templateText = TemplateText.Text;
      String replacementCharacters = ReplacementCharacters.Text;
      String seperator = SplitCharsForLabels.Text;
      String toInsertLabelText = ToInsertLabels.Text;

      if (String.IsNullOrEmpty(templateText) 
        || String.IsNullOrEmpty(replacementCharacters) 
        || String.IsNullOrEmpty(seperator) 
        || String.IsNullOrEmpty(toInsertLabelText)) {
        StatusText.Foreground = Brushes.Red;
        StatusText.Text = "Alle Felder müssen ausgefüllt sein";
        return;
      }

      // plit the labels on the seperator
      if (seperator == "\\r\\n") {
        seperator = Environment.NewLine;
      }
      var splittedLabels = toInsertLabelText.Split(new String[]{ seperator }, StringSplitOptions.RemoveEmptyEntries);
      if (!splittedLabels.Any()) {
        StatusText.Foreground = Brushes.Red;
        StatusText.Text = "Keine Labels gefunden";
        return;
      }

      // Replace the text in the template with the label and add it to the output
      StringBuilder resultText = new StringBuilder();
      foreach (var label in splittedLabels) {
        resultText.AppendLine(templateText.Replace(replacementCharacters, label));
        resultText.AppendLine();
      }

      ResultView.Text = resultText.ToString();
      StatusText.Foreground = Brushes.Black;
      StatusText.Text = "Status: Alles okay";
    }


  }
}
