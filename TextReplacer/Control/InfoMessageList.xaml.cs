using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for InfoMessageList.xaml
  /// </summary>
  public partial class InfoMessageList : UserControl {
    public InfoMessageList() {
      InitializeComponent();
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty InfoMessagesPropertyProperty =
        DependencyProperty.Register("InfoMessages", typeof(ObservableCollection<InfoMessage>), typeof(InfoMessageList), new UIPropertyMetadata());
    public ObservableCollection<InfoMessage> InfoMessages {
      get { return (ObservableCollection<InfoMessage>)GetValue(InfoMessagesPropertyProperty); }
      set { SetValue(InfoMessagesPropertyProperty, value); }
    }


    /// <summary>Removes the selected message from the list.</summary>
    /// <param name="sender">Object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void HandleDeleteMessageClick(Object sender, RoutedEventArgs e) {
      FrameworkElement element = sender as FrameworkElement;
      InfoMessage message = element?.DataContext as InfoMessage;
      if (message == null) {
        return;
      }
      InfoMessages.Remove(message);
    }
  }
}
