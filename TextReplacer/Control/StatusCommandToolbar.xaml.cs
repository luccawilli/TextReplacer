using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for StatusCommandToolbar.xaml
  /// </summary>
  public partial class StatusCommandToolbar : UserControl {

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty StatusTextProperty =
        DependencyProperty.Register("StatusText", typeof(String), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public String StatusText {
      get { return (String)GetValue(StatusTextProperty); }
      set { SetValue(StatusTextProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty StatusForegroundProperty =
        DependencyProperty.Register("StatusForeground", typeof(Brush), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public Brush StatusForeground {
      get { return (Brush)GetValue(StatusForegroundProperty); }
      set { SetValue(StatusForegroundProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty StartCommandProperty =
        DependencyProperty.Register("StartCommand", typeof(ICommand), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public ICommand StartCommand {
      get { return (ICommand)GetValue(StartCommandProperty); }
      set { SetValue(StartCommandProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty ClearCommandProperty =
        DependencyProperty.Register("ClearCommand", typeof(ICommand), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public ICommand ClearCommand {
      get { return (ICommand)GetValue(ClearCommandProperty); }
      set { SetValue(ClearCommandProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty CopyCommandProperty =
        DependencyProperty.Register("CopyCommand", typeof(ICommand), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public ICommand CopyCommand {
      get { return (ICommand)GetValue(CopyCommandProperty); }
      set { SetValue(CopyCommandProperty, value); }
    }


    public StatusCommandToolbar() {
      InitializeComponent();
    }
  }
}
