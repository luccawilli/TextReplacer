using TextReplacer.Command;

namespace TextReplacer.ViewModel {
  public class MainViewModel : BindableBase {

    private TextReplacerViewModel _textReplacerViewModel;
    public TextReplacerViewModel TextReplacerViewModel { 
      get => _textReplacerViewModel;
      set => SetProperty(ref _textReplacerViewModel, value);
    }

    private RgExViewModel _rgExViewModel;
    public RgExViewModel RgExViewModel {
      get => _rgExViewModel;
      set => SetProperty(ref _rgExViewModel, value);
    }

    private TemplateCreatorViewModel _templateCreatorViewModel;
    public TemplateCreatorViewModel TemplateCreatorViewModel {
      get => _templateCreatorViewModel;
      set => SetProperty(ref _templateCreatorViewModel, value);
    }

    /// <summary>Creates a new instance</summary>
    public MainViewModel() {
      TextReplacerViewModel = new TextReplacerViewModel();
      RgExViewModel = new RgExViewModel();
      TemplateCreatorViewModel = new TemplateCreatorViewModel();
    }

  }
}
