using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using TextReplacer.ViewModel;

namespace TextReplacer {
  /// <summary>A simple tool to replace specific strings with multiple given --> so to generate filled out templates.</summary>
  public partial class MainWindow : Window {

    public MainViewModel ViewModel { get; set; }

    public MainWindow() {
      DataContext = ViewModel = new MainViewModel();
      InitializeComponent();

      TemplateText.ToolTip = @"Hier rein kommt das Template, 
also der Text in dem gewisse Bereiche ersetzt werden sollen.
Bsp: var xxx = 123 * 90; // xxx wird ersetzt mit einem Wert aus der Liste der 'Einzufügende Labels'";

      ToInsertLabels.ToolTip = "Diese Werte werden eingefügt für das 'Ersetzungszeichen', für jeden Wert ergibt es einen eigen Template-Output";

      ReplacementCharacters.ToolTip = "Dieser Wert wird im Template ersetzt";
      //AddDefaultInfo();
    }

  }
}
