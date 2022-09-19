using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TextReplacer.ViewModel;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for TemplateList.xaml
  /// </summary>
  public partial class TemplateList : UserControl {
    public TemplateList() {
      InitializeComponent();
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty TemplatesPropertyProperty =
        DependencyProperty.Register("Templates", typeof(ObservableCollection<TemplateDto>), typeof(TemplateList), new UIPropertyMetadata());
    public ObservableCollection<TemplateDto> Templates {
      get { return (ObservableCollection<TemplateDto>)GetValue(TemplatesPropertyProperty); }
      set { SetValue(TemplatesPropertyProperty, value); }
    }

    /// <summary>Removes the selected template from the list.</summary>
    /// <param name="sender">Object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void HandleDeleteTemplateClick(Object sender, RoutedEventArgs e) {
      FrameworkElement element = sender as FrameworkElement;
      TemplateDto template = element?.DataContext as TemplateDto;
      if (template == null) {
        return;
      }
      Templates.Remove(template);
    }
  }
}
