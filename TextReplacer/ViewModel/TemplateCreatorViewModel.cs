using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class TemplateCreatorViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Variable == 1:1 *Variable == Grosser Anfang _Variable == kleiner Anfang";

    public TemplateCreatorViewModel() {
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    private String _splitCharsForLabels = "\\r\\n";
    public String SplitCharsForLabels {
      get => _splitCharsForLabels;
      set => SetProperty(ref _splitCharsForLabels, value);
    }

    private String _variableText = "77,88,99";
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

    public override void Start() {
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(VariableText)
        || String.IsNullOrEmpty(SplitChar)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }

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
      StringBuilder sb = new StringBuilder();
      foreach (String variableValue in variableValues) {
        String template = TemplateText;
        String[] values = variableValue.Split(splitChar, StringSplitOptions.None);
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

        var regex = new Regex(String.Join("|", replacements.Keys.Select(k => Regex.Escape(k))));
        template = regex.Replace(template, m => replacements[m.Value]);

        sb.AppendLine(template);
        if (HasNewLinesInBetween.HasValue && HasNewLinesInBetween.Value) {
          sb.AppendLine();
        }
      }

      ResultText = sb.ToString();
    }

    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }
  }
}
