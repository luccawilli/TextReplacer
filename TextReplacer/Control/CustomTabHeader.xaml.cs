using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for CustomTabHeader.xaml
  /// </summary>
  public partial class CustomTabHeader : UserControl {

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CustomTabHeader), new UIPropertyMetadata());
    public String Text {
      get { return (String)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CustomTabHeader), new UIPropertyMetadata());
    public ImageSource ImageSource {
      get { return (ImageSource)GetValue(ImageSourceProperty); }
      set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(CustomTabHeader), new UIPropertyMetadata(Visibility.Visible));
    public Visibility TextVisibility {
      get { return (Visibility)GetValue(TextVisibilityProperty); }
      set { SetValue(TextVisibilityProperty, value); }
    }
    public CustomTabHeader() {
      InitializeComponent();
    }
  }
}
