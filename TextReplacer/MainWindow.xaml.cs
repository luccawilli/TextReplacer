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
    }

  }
}
