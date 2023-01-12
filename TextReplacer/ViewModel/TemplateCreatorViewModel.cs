using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class TemplateCreatorViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Variable == 1:1 *Variable == Grosser Anfang _Variable == kleiner Anfang";

    public TemplateCreatorViewModel() {
      TemplateCreatorControlViewModel = new TemplateCreatorControlViewModel();
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    private TemplateCreatorControlViewModel _templateCreatorControlViewModel;
    public TemplateCreatorControlViewModel TemplateCreatorControlViewModel {
      get => _templateCreatorControlViewModel;
      set => SetProperty(ref _templateCreatorControlViewModel, value);
    }

    public String TemplateText => TemplateCreatorControlViewModel.TemplateText;

    public String SplitCharsForLabels => TemplateCreatorControlViewModel.SplitCharsForLabels;

    public String VariableText => TemplateCreatorControlViewModel.VariableText;

    public String SplitChar => TemplateCreatorControlViewModel.SplitChar;

    public String ToInsertLabels => TemplateCreatorControlViewModel.ToInsertLabels;

    public override void Start() {
      SetStatusToStandard();
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(VariableText)
        || String.IsNullOrEmpty(SplitChar)) {
        SetStatusToInfo("Bitte alles ausfüllen");
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
