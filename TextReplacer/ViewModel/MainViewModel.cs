using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TextReplacer.Command;

namespace TextReplacer.ViewModel {
  public class MainViewModel : BindableBase {

    private TextReplacerViewModel _textReplacerViewModel;
    public TextReplacerViewModel TextReplacerViewModel { 
      get => _textReplacerViewModel;
      set => SetProperty(ref _textReplacerViewModel, value);
    }

    /// <summary>Creates a new instance</summary>
    public MainViewModel() {
      TextReplacerViewModel = new TextReplacerViewModel();
    }

  }
}
