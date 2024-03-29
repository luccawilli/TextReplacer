﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for StatusCommandToolbar.xaml
  /// </summary>
  public partial class StatusCommandToolbar : UserControl {   

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty RunCommandProperty =
        DependencyProperty.Register("RunCommand", typeof(ICommand), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public ICommand RunCommand {
      get { return (ICommand)GetValue(RunCommandProperty); }
      set { SetValue(RunCommandProperty, value); }
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

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty SaveCommandProperty =
        DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(StatusCommandToolbar), new UIPropertyMetadata());
    public ICommand SaveCommand {
      get { return (ICommand)GetValue(SaveCommandProperty); }
      set { SetValue(SaveCommandProperty, value); }
    }


    public StatusCommandToolbar() {
      InitializeComponent();
    }
  }
}
