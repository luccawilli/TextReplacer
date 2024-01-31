using System.Windows;
using TextReplacer.ViewModel;

namespace TextReplacer {
  /// <summary>A simple tool to replace specific strings with multiple given --> so to generate filled out templates.</summary>
  public partial class MainWindow : Window {

    public MainViewModel ViewModel { get; set; }

    public MainWindow() {
      DataContext = ViewModel = new MainViewModel();
      InitializeComponent();
    }

        private void TextReplacerView_Loaded(object sender, RoutedEventArgs e) {

        }
    }
}
