using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {

  public class RgExControlViewModel : BindableBaseStartReplace {
    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
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

    public override string Start(string input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings) {
      String templateText = ReplacerHelper.ReplaceWorkflow(TemplateText, input);
      String regexPattern = ReplacerHelper.ReplaceWorkflow(RegexPattern, input);
      String charactersToRemove = ReplacerHelper.ReplaceWorkflow(CharactersToRemove, input);

      Regex regex = new Regex(regexPattern);
      MatchCollection matches = regex.Matches(templateText);

      Dictionary<String, String> charactersToRemoveDict = GetCharactersToRemoveDictionary(charactersToRemove, RemoveWhitespaces);
      Regex charactersToRemoveRegex = ReplacerHelper.GetReplacerRegex(charactersToRemoveDict);

      StringBuilder sb = new StringBuilder();
      foreach (Match match in matches) {
        String matchValue = ReplacerHelper.GetReplacedText(match.Value, charactersToRemoveDict, charactersToRemoveRegex);
        sb.AppendLine(matchValue);
        if (HasNewLinesInBetween.HasValue && HasNewLinesInBetween.Value) {
          sb.AppendLine();
        }
      }

      return sb.ToString();
    }

    private static Dictionary<String, String> GetCharactersToRemoveDictionary(String charactersToRemove, Boolean? removeWhitespaces) {
      var charactersToRemoveDict = charactersToRemove
        ?.Split(new String[] { "" }, StringSplitOptions.RemoveEmptyEntries)
        .ToDictionary(x => x, y => "")
        ?? new Dictionary<String, String>();
      if (removeWhitespaces ?? false) {
        charactersToRemoveDict[" "] = "";
      }

      return charactersToRemoveDict;
    }
  }
}
