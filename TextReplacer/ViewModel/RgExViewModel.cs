using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Mit dem Regex-Pattern kann Text extrahiert werden";

    public RgExViewModel() {
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
      Regex regex = new Regex(RegexPattern);
      MatchCollection matches = regex.Matches(TemplateText);

      Dictionary<String, String> charactersToRemoveDict = GetCharactersToRemoveDictionary();
      Regex charactersToRemoveRegex = ReplacerHelper.GetReplacerRegex(charactersToRemoveDict);

      StringBuilder sb = new StringBuilder();
      foreach (Match match in matches) {
        String matchValue = ReplacerHelper.GetReplacedText(match.Value, charactersToRemoveDict, charactersToRemoveRegex);
        sb.AppendLine(matchValue);
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

    private Dictionary<String, String> GetCharactersToRemoveDictionary() {
      var charactersToRemoveDict = CharactersToRemove
        ?.Split(new String[] { "" }, StringSplitOptions.RemoveEmptyEntries)
        .ToDictionary(x => x, y => "") 
        ?? new Dictionary<String, String>();
      if (RemoveWhitespaces ?? false) {
        charactersToRemoveDict[" "] = "";
      }

      return charactersToRemoveDict;
    }
  }
}
