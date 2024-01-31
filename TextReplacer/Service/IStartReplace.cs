using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Enum;

namespace TextReplacer.Service {

  /// <summary>Interface for executing replacesments.</summary>
  public interface IStartReplace {

    ReplacerType Type { get; }

    String Start(String input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings);

  }
}
