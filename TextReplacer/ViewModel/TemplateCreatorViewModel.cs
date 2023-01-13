using System;

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

    public override bool Validate() {
      SetStatusToStandard();
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(VariableText)
        || String.IsNullOrEmpty(SplitChar)) {
        SetStatusToInfo("Bitte alles ausfüllen");
        return false;
      }

      if (OutputType == Enum.OutputType.InFiles
        && (String.IsNullOrEmpty(OutputFileName) || String.IsNullOrEmpty(OutputFolderPath))) {
        SetStatusToInfo("Bitte Verzeichnis und Dateiname angeben");
        return false;
      }
      return base.Validate();
    }

    public override void Start() {
      ResultText = TemplateCreatorControlViewModel.Start("", ApplyOutputSettings);
    }

    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }
  }
}
