using System;
using System.Windows;
using System.Windows.Controls;

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
    public static readonly DependencyProperty HasNewLinesInBetweenProperty =
        DependencyProperty.Register("HasNewLinesInBetween", typeof(Boolean?), typeof(ResultTextBox), new UIPropertyMetadata(false));
    public Boolean? HasNewLinesInBetween {
      get { return (Boolean?)GetValue(HasNewLinesInBetweenProperty); }
      set { SetValue(HasNewLinesInBetweenProperty, value); }
    }

    public ResultTextBox() {
      InitializeComponent();
    }
  }
}
