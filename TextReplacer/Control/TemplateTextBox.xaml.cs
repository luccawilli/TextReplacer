using System;
using System.Windows;
using System.Windows.Controls;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for TemplateTextBox.xaml
  /// </summary>
  public partial class TemplateTextBox : UserControl {

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty TemplateTextProperty =
        DependencyProperty.Register("TemplateText", typeof(String), typeof(TemplateTextBox), new UIPropertyMetadata());
    public String TemplateText {
      get { return (String)GetValue(TemplateTextProperty); }
      set { SetValue(TemplateTextProperty, value); }
    }

    public TemplateTextBox() {
      InitializeComponent();

      TextBox.ToolTip = @"Hier rein kommt das Template, 
also der Text in dem gewisse Bereiche ersetzt werden sollen.
Bsp: var xxx = 123 * 90; // xxx wird ersetzt mit einem Wert aus der Liste der 'Einzufügende Labels'";
    }
  }
}
