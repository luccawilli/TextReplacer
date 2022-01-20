using System;
using System.Linq;
using System.Text;

namespace TextReplacer.ViewModel {
  public class TextReplacerViewModel : BaseViewModel {

    public TextReplacerViewModel() {
      SetStatusToStandard();
      AddInfo(InfoTemplateText);
    }

    public String InfoTemplateText => $"Info: {ReplacementChars} == 1:1 *{ReplacementChars} == Grosser Anfang _{ReplacementChars} == kleiner Anfang";

    private String _replacementChars = "xxx";
    public String ReplacementChars {
      get => _replacementChars;
      set => SetProperty(ref _replacementChars, value);
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

    #region Methods

    /// <summary>Create a result from the given template and replaces the replacement chars with the given insert labels.</summary>
    public override void Start() {
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

      // split the labels on the seperator
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

    public override void Clear() {
      base.Clear();
      AddInfo(InfoTemplateText);
    }
    #endregion
  }
}
