using System;
using TextReplacer.Enum;
using TextReplacer.Service;

namespace TextReplacer.Dto {
  public class TabItemElement {

    public TabItemElement(ReplacerType replacerType, BindableBaseStartReplace dataContext, System.Windows.Controls.Control content) { 
      ReplacerType = replacerType;
      DataContext = dataContext;
      Content = content;
      if (Content != null) {
        Content.DataContext = dataContext;
      }      
    }

    public ReplacerType ReplacerType { get; private set; }

    public BindableBaseStartReplace DataContext { get; private set; }

    public System.Windows.Controls.Control Content { get; private set; }

  }
}
