﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Binding;
using TextReplacer.Enum;

namespace TextReplacer.Service {
  public class BindableBaseStartReplace : BindableBase, IStartReplace, ICloneable {
    public virtual ReplacerType Type => ReplacerType.None;

    public virtual string Text { get; }

    public virtual string Start(string input, Func<StringBuilder, Dictionary<String, String>, Regex, String> ApplyOutputSettings) {
      return "";
    }

    public virtual object Clone() {
      return new BindableBaseStartReplace();
    }
  }
}
