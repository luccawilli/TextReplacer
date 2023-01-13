using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TextReplacer.Binding;
using TextReplacer.Enum;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class BaseViewModel : BindableBase {

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

    private ICommand _runCommand;

    public ICommand RunCommand {
      get {
        if (_runCommand == null) {
          _runCommand = new RelayCommand(
              param => Run()
          );
        }
        return _runCommand;
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

    private ICommand _saveCommand;
    public ICommand SaveCommand {
      get {
        if (_saveCommand == null) {
          _saveCommand = new RelayCommand(
              param => Save()
          );
        }
        return _saveCommand;
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

    private String _resultText;
    public String ResultText {
      get => _resultText;
      set => SetProperty(ref _resultText, value);
    }

    private OutputType _outputType = OutputType.Normal;
    public OutputType OutputType {
      get => _outputType;
      set => SetProperty(ref _outputType, value);
    }

    private String _outputFolderPath;
    public String OutputFolderPath {
      get => _outputFolderPath;
      set => SetProperty(ref _outputFolderPath, value);
    }

    private String _outputFileName;
    public String OutputFileName {
      get => _outputFileName;
      set => SetProperty(ref _outputFileName, value);
    }

    private Boolean? _outputOverrideExistingFiles = false;
    public Boolean? OutputOverrideExistingFiles {
      get => _outputOverrideExistingFiles;
      set => SetProperty(ref _outputOverrideExistingFiles, value);
    }


    #region Methods

    public virtual Boolean Validate() {
      if (OutputType == Enum.OutputType.InFiles
        && (String.IsNullOrEmpty(OutputFileName) || String.IsNullOrEmpty(OutputFolderPath))) {
        SetStatusToInfo("Bitte Verzeichnis und Dateiname angeben");
        return false;
      }
      return true;
    }

    public void Run() {
      if (Validate()) {
        Start();
      }
    }

    public virtual void Start() {

    }

    public virtual void Save() { }

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
      SetStatusInfo("Kopiert!");
    }

    public String ApplyOutputSettings(StringBuilder sb, Dictionary<String, String> replacements, Regex regex) {
      return ReplacerHelper.ApplyOutputSettings(sb, replacements, regex, OutputType, OutputFileName, OutputFolderPath, OutputOverrideExistingFiles);
    }

    protected void SetStatusToStandard() {
      StatusForeground = Brushes.Black;
      StatusText = "Alles okay";
    }
    private void SetStatusDanger(string text) {
      StatusForeground = Brushes.Red;
      StatusText = text;
    }
    private void SetStatusInfo(string text) {
      StatusForeground = Brushes.Black;
      StatusText = text;
    }

    protected void SetStatusToInfo(String text) {
      SetStatusDanger(text);
      InfoMessages.Add(new InfoMessage() { Brush = Brushes.Red, Message = text });
    }

    protected void AddInfo(String text) {
      InfoMessages.Add(new InfoMessage() { Brush = InfoMessage.TextReplacerBlue, Message = text });
    }
    #endregion
  }
}
