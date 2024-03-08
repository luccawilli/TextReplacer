using System;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class TemplateCreatorViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Variable == 1:1 *Variable == Grosser Anfang _Variable == kleiner Anfang";

    public TemplateCreatorViewModel(MainViewModel main) : base(main) {
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

    public override void Save() {
      var clone = (TemplateCreatorControlViewModel)TemplateCreatorControlViewModel.Clone();
      _main.Templates.TemplateCreatorTemplates.Add(clone);
      base.AddSave(clone);
    }

    public override void Apply(BindableBaseStartReplace b) {
      if (b is TemplateCreatorControlViewModel t) {
        TemplateCreatorControlViewModel = t;
      }
    }
    public override void Remove(BindableBaseStartReplace b) {
      if (b is TemplateCreatorControlViewModel t) {
        _main.Templates.TemplateCreatorTemplates.Remove(t);
      }
    }
  }
}
