using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReplacer.Service {

  /// <summary>Interface for executing replacesments.</summary>
  public interface IStartReplace {

    String Start(String input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings);

  }
}
