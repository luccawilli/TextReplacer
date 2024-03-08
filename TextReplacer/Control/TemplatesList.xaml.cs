using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TextReplacer.Service;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for TemplatesList.xaml
  /// </summary>
  public partial class TemplatesList : UserControl {
    public TemplatesList() {
      InitializeComponent();
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty TemplatesPropertyProperty =
        DependencyProperty.Register("Templates", typeof(ObservableCollection<BindableBaseStartReplace>), typeof(TemplatesList), new UIPropertyMetadata());
    public ObservableCollection<BindableBaseStartReplace> Templates {
      get { return (ObservableCollection<BindableBaseStartReplace>)GetValue(TemplatesPropertyProperty); }
      set { SetValue(TemplatesPropertyProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty RemoveTemplateCommandProperty =
        DependencyProperty.Register("RemoveTemplateCommand", typeof(ICommand), typeof(TemplatesList), new UIPropertyMetadata());
    public ICommand RemoveTemplateCommand {
      get { return (ICommand)GetValue(RemoveTemplateCommandProperty); }
      set { SetValue(RemoveTemplateCommandProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty ApplyTemplateCommandProperty =
        DependencyProperty.Register("ApplyTemplateCommand", typeof(ICommand), typeof(TemplatesList), new UIPropertyMetadata());
    public ICommand ApplyTemplateCommand {
      get { return (ICommand)GetValue(ApplyTemplateCommandProperty); }
      set { SetValue(ApplyTemplateCommandProperty, value); }
    }

    private void HandleRemoveTemplate(Object sender, RoutedEventArgs e) {
      RemoveTemplateCommand?.Execute(sender);
    }

    private void HandleApplyTemplate(Object sender, RoutedEventArgs e) {
      ApplyTemplateCommand?.Execute(sender);
    }
  }
}
