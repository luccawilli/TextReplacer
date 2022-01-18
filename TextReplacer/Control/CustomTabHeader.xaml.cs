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


    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register("ImageWidth", typeof(Int32), typeof(CustomTabHeader), new UIPropertyMetadata(20));
    public Int32 ImageWidth {
      get { return (Int32)GetValue(ImageWidthProperty); }
      set { SetValue(ImageWidthProperty, value); }
    }

    public CustomTabHeader() {
      InitializeComponent();
    }
  }
}
