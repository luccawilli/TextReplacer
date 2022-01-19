using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextReplacer.Command;

namespace TextReplacer.ViewModel {
  public class RgExViewModel : BaseViewModel {

    private String _regexPattern;
    public String RegexPattern {
      get => _regexPattern;
      set => SetProperty(ref _regexPattern, value);
    }

    public override void Start() {
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
