using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    private String _infoTemplateText = "Info: Mit dem Regex-Pattern kann Text extrahiert werden";

    public RgExViewModel() {
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    private String _regexPattern;
    public String RegexPattern {
      get => _regexPattern;
      set => SetProperty(ref _regexPattern, value);
    }

    private Boolean? _hasNewLinesInBetween = false;
    public Boolean? HasNewLinesInBetween {
      get => _hasNewLinesInBetween;
      set => SetProperty(ref _hasNewLinesInBetween, value);
    }

    public override void Start() {
      if (String.IsNullOrEmpty(TemplateText)
       || String.IsNullOrEmpty(RegexPattern)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }

      Regex regex = new Regex(RegexPattern);
      MatchCollection matches = regex.Matches(TemplateText);
      StringBuilder sb = new StringBuilder();
      foreach (Match match in matches) {
        sb.AppendLine(match.Value);
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
