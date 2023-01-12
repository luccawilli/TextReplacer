using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TextReplacer.Binding;

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

    private WorkFlowViewModel _workFlowViewModel;
    public WorkFlowViewModel WorkFlowViewModel {
      get => _workFlowViewModel;
      set => SetProperty(ref _workFlowViewModel, value);
    }

    private Visibility _textVisibility = Visibility.Visible;
    public Visibility TextVisibility {
      get => _textVisibility;
      set => SetProperty(ref _textVisibility, value);
    }

    private ObservableCollection<TabItem> _tabItems;
    public ObservableCollection<TabItem> TabItems {
      get => _tabItems;
      set => SetProperty(ref _tabItems, value);
    }

    /// <summary>Creates a new instance</summary>
    public MainViewModel() {
      TextReplacerViewModel = new TextReplacerViewModel();
      RgExViewModel = new RgExViewModel();
      TemplateCreatorViewModel = new TemplateCreatorViewModel();
      WorkFlowViewModel = new WorkFlowViewModel();
    }
  }
}
