using System;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class TextReplacerControlViewModel : BindableBaseStartReplace {

    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
    }

    private String _replacementChars = "xxx";
    public String ReplacementChars {
      get => _replacementChars;
      set => SetProperty(ref _replacementChars, value);
    }

    private String _splitCharsForLabels = "\\r\\n";
    public String SplitCharsForLabels {
      get => _splitCharsForLabels;
      set => SetProperty(ref _splitCharsForLabels, value);
    }

    private String _toInsertLabels;
    public String ToInsertLabels {
      get => _toInsertLabels;
      set => SetProperty(ref _toInsertLabels, value);
    }
  }
}
