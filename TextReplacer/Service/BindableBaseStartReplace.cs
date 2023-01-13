using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Binding;

namespace TextReplacer.Service {
  public class BindableBaseStartReplace : BindableBase, IStartReplace {
    public virtual string Start(string input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings) {
      return "";
    }
  }
}
