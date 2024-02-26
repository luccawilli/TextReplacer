using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for ResultTextBox.xaml
  /// </summary>
  public partial class ResultTextBox : UserControl {

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty ResultTextProperty =
        DependencyProperty.Register("ResultText", typeof(String), typeof(ResultTextBox), new UIPropertyMetadata());
    public String ResultText {
      get { return (String)GetValue(ResultTextProperty); }
      set { SetValue(ResultTextProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty StatusTextProperty =
        DependencyProperty.Register("StatusText", typeof(String), typeof(ResultTextBox), new UIPropertyMetadata());
    public String StatusText {
      get { return (String)GetValue(StatusTextProperty); }
      set { SetValue(StatusTextProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty StatusForegroundProperty =
        DependencyProperty.Register("StatusForeground", typeof(Brush), typeof(ResultTextBox), new UIPropertyMetadata());
    public Brush StatusForeground {
      get { return (Brush)GetValue(StatusForegroundProperty); }
      set { SetValue(StatusForegroundProperty, value); }
    }

    public ResultTextBox() {
      InitializeComponent();
    }
  }
}
