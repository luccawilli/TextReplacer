using System;
using TextReplacer.Enum;

namespace TextReplacer.Dto {
  public class TabItemElement {

    public TabItemElement(ReplacerType replacerType, Object dataContext, System.Windows.Controls.Control content) { 
      ReplacerType = replacerType;
      DataContext = dataContext;
      Content = content;
      if (Content != null) {
        Content.DataContext = dataContext;
      }      
    }

    public ReplacerType ReplacerType { get; private set; }

    public Object DataContext { get; private set; }

    public System.Windows.Controls.Control Content { get; private set; }

  }
}
