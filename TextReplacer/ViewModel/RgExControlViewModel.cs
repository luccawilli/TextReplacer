using System;
using TextReplacer.Binding;

namespace TextReplacer.ViewModel {

  public class RgExControlViewModel : BindableBase {
    private String _templateText;
    public String TemplateText {
      get => _templateText;
      set => SetProperty(ref _templateText, value);
    }

    private String _regexPattern;
    public String RegexPattern {
      get => _regexPattern;
      set => SetProperty(ref _regexPattern, value);
    }

    private String _charactersToRemove;
    public String CharactersToRemove {
      get => _charactersToRemove;
      set => SetProperty(ref _charactersToRemove, value);
    }

    private Boolean? _hasNewLinesInBetween = false;
    public Boolean? HasNewLinesInBetween {
      get => _hasNewLinesInBetween;
      set => SetProperty(ref _hasNewLinesInBetween, value);
    }

    private Boolean? _removeWhitespaces = false;
    public Boolean? RemoveWhitespaces {
      get => _removeWhitespaces;
      set => SetProperty(ref _removeWhitespaces, value);
    }
  }
}
