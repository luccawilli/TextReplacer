using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Mit dem Regex-Pattern kann Text extrahiert werden";

    public RgExViewModel() {
      AddInfo(_infoTemplateText);
    }

    private String _regexPattern;
    public String RegexPattern {
      get => _regexPattern;
      set => SetProperty(ref _regexPattern, value);
    }

    private String _charactersToRemove;
    public String CharactersToRemove {
      get => _charactersToRemove;
      set => SetProperty(ref _charactersToRemove, value);
    }

    private Boolean? _hasNewLinesInBetween = false;
    public Boolean? HasNewLinesInBetween {
      get => _hasNewLinesInBetween;
      set => SetProperty(ref _hasNewLinesInBetween, value);
    }

    private Boolean? _removeWhitespaces = false;
    public Boolean? RemoveWhitespaces {
      get => _removeWhitespaces;
      set => SetProperty(ref _removeWhitespaces, value);
    }

    public override void Start() {
      if (String.IsNullOrEmpty(TemplateText)
       || String.IsNullOrEmpty(RegexPattern)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }

      Regex regex = new Regex(RegexPattern);
      MatchCollection matches = regex.Matches(TemplateText);

      Dictionary<String, String> charactersToRemoveDict = GetCharactersToRemoveDictionary();
      Regex charactersToRemoveRegex = GetReplacerRegex(charactersToRemoveDict);

      StringBuilder sb = new StringBuilder();
      foreach (Match match in matches) {
        String matchValue = GetReplacedText(match.Value, charactersToRemoveDict, charactersToRemoveRegex);
        sb.AppendLine(matchValue);
        if (HasNewLinesInBetween.HasValue && HasNewLinesInBetween.Value) {
          sb.AppendLine();
        }
      }
      ResultText = sb.ToString();
    }

    public override void Save() {
      var template = new TemplateDto() {
        Name = "New Template",
        DisplayText = RegexPattern,
      };
      Templates.Add(template);
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
