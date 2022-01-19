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

    /// <summary>Creates a new instance</summary>
    public MainViewModel() {
      SetStatusToStandard();
      AddDefaultInfo();
    }

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

    public String InfoTemplateText => $"Info: {ReplacementChars} == 1:1 *{ReplacementChars} == Grosser Anfang _{ReplacementChars} == kleiner Anfang";

    public ObservableCollection<InfoMessage> InfoMessages { get; set; } = new ObservableCollection<InfoMessage>();

    private String _replacementChars;
    public String ReplacementChars {
      get => _replacementChars;
      set => SetProperty(ref _replacementChars, value);
    }

    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
    }

    private String _splitCharsForLabels = "\\r\\n";
    public String SplitCharsForLabels {
      get => _splitCharsForLabels;
      set => SetProperty(ref _splitCharsForLabels, value);
    }

    private String _toInsertLabels;
    public String ToInsertLabels {
      get => _toInsertLabels;
      set => SetProperty(ref _toInsertLabels, value);
    }

    private Boolean? _hasNewLinesInBetween = false;
    public Boolean? HasNewLinesInBetween {
      get => _hasNewLinesInBetween;
      set => SetProperty(ref _hasNewLinesInBetween, value);
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

    #region Methods

    /// <summary>Handles the click on the start button</summary>
    /// <param name="sender">Object</param>
    /// <param name="e">RoutedEventArgs</param>
    public void Start() {
      String templateText = TemplateText;
      String replacementCharacters = ReplacementChars;
      String seperator = SplitCharsForLabels;
      String toInsertLabelText = ToInsertLabels;
      Boolean? addNewLines = HasNewLinesInBetween;

      if (String.IsNullOrEmpty(templateText)
        || String.IsNullOrEmpty(replacementCharacters)
        || String.IsNullOrEmpty(seperator)
        || String.IsNullOrEmpty(toInsertLabelText)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }

      // plit the labels on the seperator
      if (seperator == "\\r\\n") {
        seperator = Environment.NewLine;
      }
      var splittedLabels = toInsertLabelText.Split(new String[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
      if (!splittedLabels.Any()) {
        SetStatusToInfo("Keine Labels gefunden");
        return;
      }

      // Replace the text in the template with the label and add it to the output
      StringBuilder resultText = new StringBuilder();
      foreach (var label in splittedLabels) {
        String text = templateText.Replace("*" + replacementCharacters, Char.ToUpper(label[0]) + label.Substring(1));
        text = text.Replace("_" + replacementCharacters, Char.ToLower(label[0]) + label.Substring(1));
        resultText.AppendLine(text.Replace(replacementCharacters, label));
        if (addNewLines.HasValue && addNewLines.Value) {
          resultText.AppendLine();
        }
      }

      ResultText = resultText.ToString();
      SetStatusToStandard();
    }

    public void Clear() {
      ResultText = null;
      InfoMessages.Clear();
      AddDefaultInfo();
      SetStatusToStandard();
    }

    private void SetStatusToInfo(String text) {
      StatusForeground = Brushes.Red;
      StatusText = text;

      InfoMessages.Add(new InfoMessage() { Brush = Brushes.Red, Message = text });
    }

    private void SetStatusToStandard() {
      StatusForeground = Brushes.Black;
      StatusText = "Status: Alles okay";
    }

    private void AddDefaultInfo() {
      InfoMessages.Add(new InfoMessage() { Brush = InfoMessage.TextReplacerBlue, Message = InfoTemplateText });
    }
    #endregion
  }
}
