using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class TextReplacerViewModel : BaseViewModel {

    public TextReplacerViewModel() {
      TextReplacerControlViewModel = new TextReplacerControlViewModel();
      SetStatusToStandard();
      AddInfo(InfoTemplateText);
    }

    public String InfoTemplateText => $"Info: {ReplacementChars} == 1:1 *{ReplacementChars} == Grosser Anfang _{ReplacementChars} == kleiner Anfang";

    private TextReplacerControlViewModel _textReplacerControlViewModel;
    public TextReplacerControlViewModel TextReplacerControlViewModel {
      get => _textReplacerControlViewModel;
      set => SetProperty(ref _textReplacerControlViewModel, value);
    }

    public String TemplateText => TextReplacerControlViewModel.TemplateText;

    public String ReplacementChars => TextReplacerControlViewModel.ReplacementChars;

    public String SplitCharsForLabels => TextReplacerControlViewModel.SplitCharsForLabels;

    public String ToInsertLabels => TextReplacerControlViewModel.ToInsertLabels;

    #region Methods

    /// <summary>Create a result from the given template and replaces the replacement chars with the given insert labels.</summary>
    public override void Start() {
      SetStatusToStandard();
      String templateText = TemplateText;
      String replacementCharacters = ReplacementChars;
      String seperator = SplitCharsForLabels;
      String toInsertLabelText = ToInsertLabels;

      if (String.IsNullOrEmpty(templateText)
        || String.IsNullOrEmpty(replacementCharacters)
        || String.IsNullOrEmpty(seperator)
        || String.IsNullOrEmpty(toInsertLabelText)) {
        SetStatusToInfo("Bitte alles ausfüllen");
        return;
      }

      if (OutputType == Enum.OutputType.InFiles
        && (String.IsNullOrEmpty(OutputFileName) || String.IsNullOrEmpty(OutputFolderPath))) {
        SetStatusToInfo("Bitte Verzeichnis und Dateiname angeben");
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
        StringBuilder sb = new StringBuilder();

        Dictionary<String, String> replacements = GetReplacements(new string[] { replacementCharacters }, new string[] { label });
        Regex regex = GetReplacerRegex(replacements);
        String text = GetReplacedText(templateText, replacements, regex);
        sb.AppendLine(text);

        resultText.Append(ApplyOutputSettings(sb, replacements, regex, OutputFileName));
      }

      ResultText = resultText.ToString();
    }

    public override void Clear() {
      base.Clear();
      AddInfo(InfoTemplateText);
    }
    #endregion
  }
}
