using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class TemplateCreatorControlViewModel : BindableBaseStartReplace {

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

    private String _variableText = "1,2,3";
    public String VariableText {
      get => _variableText;
      set => SetProperty(ref _variableText, value);
    }

    private String _splitChar = ",";
    public String SplitChar {
      get => _splitChar;
      set => SetProperty(ref _splitChar, value);
    }

    private String _toInsertLabels;
    public String ToInsertLabels {
      get => _toInsertLabels;
      set => SetProperty(ref _toInsertLabels, value);
    }

    public override string Start(string input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings) {
      String[] splitChar = new String[] { SplitChar };
      String[] variables = VariableText.Split(splitChar, StringSplitOptions.None)
        .Distinct()
        .ToArray();

      String seperator = SplitCharsForLabels;
      // split the labels on the seperator
      if (seperator == "\\r\\n") {
        seperator = Environment.NewLine;
      }
      List<String> variableValues = ToInsertLabels.Split(new String[] { seperator }, StringSplitOptions.None).ToList();
      StringBuilder resultSB = new StringBuilder();
      foreach (String variableValue in variableValues) {
        StringBuilder sb = new StringBuilder();
        String template = TemplateText;
        String[] values = variableValue.Split(splitChar, StringSplitOptions.None);
        Dictionary<String, String> replacements = ReplacerHelper.GetReplacements(variables, values);

        Regex regex = ReplacerHelper.GetReplacerRegex(replacements);
        template = ReplacerHelper.GetReplacedText(template, replacements, regex);

        sb.AppendLine(template);

        resultSB.Append(ApplyOutputSettings(sb, replacements, regex));
      }

      return resultSB.ToString();
    }
  }
}
