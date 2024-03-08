using System;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Mit dem Regex-Pattern kann Text extrahiert werden";

    public RgExViewModel(MainViewModel main) : base(main) {
      RgExControlViewModel = new RgExControlViewModel();
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    private RgExControlViewModel _rgExControlViewModel;
    public RgExControlViewModel RgExControlViewModel {
      get => _rgExControlViewModel;
      set => SetProperty(ref _rgExControlViewModel, value);
    }

    public String TemplateText => RgExControlViewModel.TemplateText;
    public String RegexPattern => RgExControlViewModel.RegexPattern;
    public String CharactersToRemove => RgExControlViewModel.CharactersToRemove;
    public Boolean? HasNewLinesInBetween => RgExControlViewModel.HasNewLinesInBetween;
    public Boolean? RemoveWhitespaces => RgExControlViewModel.RemoveWhitespaces;

    public override bool Validate() {
      SetStatusToStandard();
      if (String.IsNullOrEmpty(TemplateText)
       || String.IsNullOrEmpty(RegexPattern)) {
        SetStatusToInfo("Bitte alles ausfüllen");
        return false;
      }
      return base.Validate();
    }

    public override void Start() {
      ResultText = RgExControlViewModel.Start("", ApplyOutputSettings);
    }

    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }

    public override void Save() {
      _main.Templates.RgExTemplates.Add(RgExControlViewModel);
      base.AddSave(RgExControlViewModel);
    }
  }
}
