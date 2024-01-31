using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Enum;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class TextReplacerControlViewModel : BindableBaseStartReplace {

    public override ReplacerType Type => ReplacerType.TextReplacer;

    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
    }

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

    public override string Start(string input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings) {
      String templateText = ReplacerHelper.ReplaceWorkflow(TemplateText, input);
      String replacementChars = ReplacerHelper.ReplaceWorkflow(ReplacementChars, input);
      String splitCharsForLabels = ReplacerHelper.ReplaceWorkflow(SplitCharsForLabels, input);
      String toInsertLabels = ReplacerHelper.ReplaceWorkflow(ToInsertLabels, input);

      String seperator = ReplacerHelper.GetSeperator(splitCharsForLabels);
      String[] splittedLabels = ReplacerHelper.GetSplittedLabels(toInsertLabels, seperator);

      // Replace the text in the template with the label and add it to the output
      StringBuilder resultText = new StringBuilder();
      foreach (var label in splittedLabels) {
        StringBuilder sb = new StringBuilder();

        Dictionary<String, String> replacements = ReplacerHelper.GetReplacements(new string[] { replacementChars }, new string[] { label });
        Regex regex = ReplacerHelper.GetReplacerRegex(replacements);
        String text = ReplacerHelper.GetReplacedText(templateText, replacements, regex);
        sb.AppendLine(text);

        resultText.Append(ApplyOutputSettings(sb, replacements, regex));
      }

      return resultText.ToString();
    }

  }
}
