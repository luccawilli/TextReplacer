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

    public override void Start() {
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(VariableText)
        || String.IsNullOrEmpty(SplitChar)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }

      if (OutputType == Enum.OutputType.InFiles
        && (String.IsNullOrEmpty(OutputFileName) || String.IsNullOrEmpty(OutputFolderPath))) {
        SetStatusToInfo("Bitte Verzeichnis und Dateiname angeben");
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
      StringBuilder resultSB = new StringBuilder();
      foreach (String variableValue in variableValues) {
        StringBuilder sb = new StringBuilder();
        String template = TemplateText;
        String[] values = variableValue.Split(splitChar, StringSplitOptions.None);
        Dictionary<String, String> replacements = GetReplacements(variables, values);

        Regex regex = GetReplacerRegex(replacements);
        template = GetReplacedText(template, replacements, regex);

        sb.AppendLine(template);

        resultSB.Append(ApplyOutputSettings(sb, replacements, regex, OutputFileName));
      }

      ResultText = resultSB.ToString();
    }


    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }
  }
}
