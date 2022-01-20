using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    public RgExViewModel() {
      SetStatusToStandard();
      AddInfo("Info: Mit dem Regex-Pattern kann Text extrahiert werden");
    }

    private String _regexPattern;
    public String RegexPattern {
      get => _regexPattern;
      set => SetProperty(ref _regexPattern, value);
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
      }
      ResultText = sb.ToString();
    }
  }
}
