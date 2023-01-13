using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TextReplacer.Dto;

namespace TextReplacer {
  /// <summary>
  /// Interaction logic for TextReplacerView.xaml
  /// </summary>
  public partial class WorkFlowView : UserControl {
    public WorkFlowView() {
      InitializeComponent();
    }

    private void HandleTabItemPreviewMouseMove(object sender, MouseEventArgs e) {
      if (!(e.Source is TabItem tabItem)) {
        return;
      }

      if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed) {
        DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
      }
    }

    private void HandleTabItemDrop(object sender, DragEventArgs e) {
      if (e.Source is TabItem tabItemTarget &&
          e.Data.GetData(typeof(TabItem)) is TabItem tabItemSource) {
        Int32 targetIndex = ReplacerTabControl.Items.IndexOf(tabItemTarget.Content);
        if (targetIndex == ReplacerTabControl.Items.Count - 1) {
          return;
        }
        ObservableCollection<TabItemElement> itemSource = ReplacerTabControl.ItemsSource as ObservableCollection<TabItemElement>;
        if (itemSource == null) {
          return;
        }
        TabItemElement element = tabItemSource.Content as TabItemElement;
        if (element == null || element.ReplacerType == Enum.ReplacerType.None) {
          return;
        }
        itemSource.Remove(element);
        itemSource.Insert(targetIndex, element);
        tabItemSource.IsSelected = true;
      }
    }
  }
}
