using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TextReplacer.Binding;
using TextReplacer.Enum;

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

    public Boolean CreateFile(Boolean overrideExistingFiles, String fileName, String content) {
      if (File.Exists(fileName) && !overrideExistingFiles) {
        return false;
      }
      try {
        File.WriteAllText(fileName, content);
      }
      catch (Exception) {
        return false;
      }
      return true;
    }

    /// <summary>Applies the output settings to the given values.</summary>
    /// <param name="sb">The text to write down.</param>
    /// <param name="filePath">The path for file.</param>
    /// <returns>The string to add in the result.</returns>
    public String ApplyOutputSettings(StringBuilder sb, Dictionary<String, String> replacements, Regex regex, String fileName) {
      switch (OutputType) {
        case OutputType.WithNewLines:
          sb.AppendLine();
          return sb.ToString();
        case OutputType.InFiles:
          fileName = GetReplacedText(fileName, replacements, regex);
          String filePath = Path.Combine(OutputFolderPath, fileName);

          Boolean fileCreated = CreateFile(OutputOverrideExistingFiles ?? false, filePath, sb.ToString());
          String prefix = fileCreated ? "Erstellt:" : "Nicht erstellt:";
          sb.Clear();
          sb.AppendLine(prefix + " " + filePath);
          return sb.ToString();
        default:
          return sb.ToString();
      }
    }

    /// <summary>Replace the variables with the replacements.</summary>
    /// <param name="template"></param>
    /// <param name="replacements"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static String GetReplacedText(String template, Dictionary<String, String> replacements, Regex regex) {
      template = regex.Replace(template, m => replacements[m.Value]);
      return template;
    }

    /// <summary>Gets the replacer reges for replacements.</summary>
    /// <param name="replacements"></param>
    /// <returns></returns>
    public static Regex GetReplacerRegex(Dictionary<String, String> replacements) {
      return new Regex(String.Join("|", replacements.Keys.Select(k => Regex.Escape(k))));
    }

    /// <summary>Gets the replacements for the given variables and their values.</summary>
    /// <param name="variables"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Dictionary<String, String> GetReplacements(String[] variables, String[] values) {
      Dictionary<String, String> replacements = new Dictionary<String, String>();
      for (Int32 i = 0; i < variables.Length; i++) {
        if (values.Length < i) {
          continue;
        }

        String variable = variables[i];
        String value = values[i];
        replacements.Add(variable, value);
        replacements.Add("*" + variable, Char.ToUpper(value[0]) + value.Substring(1));
        replacements.Add("_" + variable, Char.ToLower(value[0]) + value.Substring(1));
      }

      return replacements;
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
