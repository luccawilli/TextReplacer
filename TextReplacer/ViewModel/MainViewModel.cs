using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TextReplacer.ViewModel {
  public class MainViewModel /*: INotifyPropertyChanged*/ {

    public MainViewModel() { }

    public ObservableCollection<InfoMessage> InfoMessages { get; set; }

    private String _replacementChars;
    public String ReplacementChars {
      get => _replacementChars;
      set => _replacementChars = value;
    }

    //public event PropertyChangedEventHandler PropertyChanged;
  }
}
