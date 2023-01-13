using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Service;

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

    public override bool Validate() {
      SetStatusToStandard();
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(ReplacementChars)
        || String.IsNullOrEmpty(SplitCharsForLabels)
        || String.IsNullOrEmpty(ToInsertLabels)) {
        SetStatusToInfo("Bitte alles ausfüllen");
        return false;
      }
      String seperator = ReplacerHelper.GetSeperator(SplitCharsForLabels);
      string[] splittedLabels = GetSplittedLabels(ToInsertLabels, seperator);
      if (!splittedLabels.Any()) {
        SetStatusToInfo("Keine Labels gefunden");
        return false;
      }
      return base.Validate();
    }

    /// <summary>Create a result from the given template and replaces the replacement chars with the given insert labels.</summary>
    public override void Start() {
      String seperator = ReplacerHelper.GetSeperator(SplitCharsForLabels);
      String[] splittedLabels = GetSplittedLabels(ToInsertLabels, seperator);

      // Replace the text in the template with the label and add it to the output
      StringBuilder resultText = new StringBuilder();
      foreach (var label in splittedLabels) {
        StringBuilder sb = new StringBuilder();

        Dictionary<String, String> replacements = ReplacerHelper.GetReplacements(new string[] { ReplacementChars }, new string[] { label });
        Regex regex = ReplacerHelper.GetReplacerRegex(replacements);
        String text = ReplacerHelper.GetReplacedText(TemplateText, replacements, regex);
        sb.AppendLine(text);

        resultText.Append(ApplyOutputSettings(sb, replacements, regex));
      }

      ResultText = resultText.ToString();
    }

    private static String[] GetSplittedLabels(String toInsertLabels, String seperator) {
      return toInsertLabels.Split(new String[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
    }

    public override void Clear() {
      base.Clear();
      AddInfo(InfoTemplateText);
    }
    #endregion
  }
}
