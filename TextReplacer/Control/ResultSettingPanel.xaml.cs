using System;
using System.Windows;
using System.Windows.Controls;
using TextReplacer.Enum;

namespace TextReplacer.Control {
  /// <summary>
  /// Interaction logic for ResultSettingPanel.xaml
  /// </summary>
  public partial class ResultSettingPanel : UserControl {

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty OutputTypeProperty =
        DependencyProperty.Register("OutputType", typeof(OutputType), typeof(ResultSettingPanel), new UIPropertyMetadata(OutputType.Normal));
    public OutputType OutputType {
      get { return (OutputType)GetValue(OutputTypeProperty); }
      set { SetValue(OutputTypeProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty OutputFolderPathProperty =
        DependencyProperty.Register("OutputFolderPath", typeof(String), typeof(ResultSettingPanel));
    public String OutputFolderPath {
      get { return (String)GetValue(OutputFolderPathProperty); }
      set { SetValue(OutputFolderPathProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty OutputFileNameProperty =
        DependencyProperty.Register("OutputFileName", typeof(String), typeof(ResultSettingPanel));
    public String OutputFileName {
      get { return (String)GetValue(OutputFileNameProperty); }
      set { SetValue(OutputFileNameProperty, value); }
    }

    // The dependency property which will be accessible on the UserControl
    public static readonly DependencyProperty OutputOverrideExistingFilesProperty =
        DependencyProperty.Register("OutputOverrideExistingFiles", typeof(Boolean?), typeof(ResultSettingPanel), new UIPropertyMetadata(false));
    public Boolean? OutputOverrideExistingFiles {
      get { return (Boolean?)GetValue(OutputOverrideExistingFilesProperty); }
      set { SetValue(OutputOverrideExistingFilesProperty, value); }
    }

    public ResultSettingPanel() {
      InitializeComponent();
    }

    private void HandleChooseOutputFolderPathClick(object sender, RoutedEventArgs e) {
      if (OutputType != OutputType.InFiles) {
        return;
      }
      System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
      folderDlg.SelectedPath = OutputFolderPath;
      if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
        OutputFolderPath = folderDlg.SelectedPath;
      }
    }
  }
}
