using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TextReplacer.Binding;

namespace TextReplacer.ViewModel {
  public class BaseViewModel : BindableBase {

    private ICommand _startCommand;

    public ICommand StartCommand {
      get {
        if (_startCommand == null) {
          _startCommand = new RelayCommand(
              param => Start()
          );
        }
        return _startCommand;
      }
    }

    private ICommand _clearCommand;

    public ICommand ClearCommand {
      get {
        if (_clearCommand == null) {
          _clearCommand = new RelayCommand(
              param => Clear()
          );
        }
        return _clearCommand;
      }
    }

    private ICommand _copyCommand;
    public ICommand CopyCommand {
      get {
        if (_copyCommand == null) {
          _copyCommand = new RelayCommand(
              param => Copy()
          );
        }
        return _copyCommand;
      }
    }

    public ObservableCollection<InfoMessage> InfoMessages { get; set; } = new ObservableCollection<InfoMessage>();

    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
    }

    private String _resultText;
    public String ResultText {
      get => _resultText;
      set => SetProperty(ref _resultText, value);
    }

    private Brush _statusForeground;
    public Brush StatusForeground {
      get => _statusForeground;
      set => SetProperty(ref _statusForeground, value);
    }

    private String _statusText;
    public String StatusText {
      get => _statusText;
      set => SetProperty(ref _statusText, value);
    }

    private Boolean? _hasNewLinesInBetween = false;
    public Boolean? HasNewLinesInBetween {
      get => _hasNewLinesInBetween;
      set => SetProperty(ref _hasNewLinesInBetween, value);
    }

    #region Methods

    public virtual void Start() { }

    public virtual void Clear() {
      ResultText = null;
      InfoMessages.Clear();
      SetStatusToStandard();
    }
    public virtual void Copy() {
      if (String.IsNullOrEmpty(ResultText)) {
        return;
      }
      Clipboard.SetText(ResultText);
      SetStatusInfo("Status: Kopiert!");
    }

    protected void SetStatusToStandard() {
      StatusForeground = Brushes.Black;
      StatusText = "Status: Alles okay";
    }

    protected void SetStatusToInfo(String text) {
      SetStatusDanger(text);

      InfoMessages.Add(new InfoMessage() { Brush = Brushes.Red, Message = text });
    }

    private void SetStatusDanger(string text) {
      StatusForeground = Brushes.Red;
      StatusText = text;
    }
    private void SetStatusInfo(string text) {
      StatusForeground = Brushes.Black;
      StatusText = text;
    }

    protected void AddInfo(String text) {
      InfoMessages.Add(new InfoMessage() { Brush = InfoMessage.TextReplacerBlue, Message = text });
    }
    #endregion
  }
}
