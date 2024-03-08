using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using TextReplacer.Binding;
using TextReplacer.Dto;

namespace TextReplacer.ViewModel {
  public class MainViewModel : BindableBase {

    const string TemplatesFileName = "TextReplacerSavedTemplates.json";

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

    public TemplateSaveDto Templates { get; set; }

    /// <summary>Creates a new instance</summary>
    public MainViewModel() {
      Templates = new TemplateSaveDto();
      TextReplacerViewModel = new TextReplacerViewModel(this);
      RgExViewModel = new RgExViewModel(this);
      TemplateCreatorViewModel = new TemplateCreatorViewModel(this);
      WorkFlowViewModel = new WorkFlowViewModel(this);
    }

    public void LoadSettings() {      
      if (!File.Exists(TemplatesFileName)) {
        return;        
      }
      Templates = JsonSerializer.Deserialize<TemplateSaveDto>(File.ReadAllText(TemplatesFileName)) ?? new TemplateSaveDto();
      Templates.TextReplacerTemplates.ForEach(x => TextReplacerViewModel.Templates.Add(x));
      Templates.RgExTemplates.ForEach(x => RgExViewModel.Templates.Add(x));
      Templates.TemplateCreatorTemplates.ForEach(x => TemplateCreatorViewModel.Templates.Add(x));
      Templates.WorkFlowTemplates.Select(x => new WorkFlowControlViewModel() { TabItems = x.TabItems }).ToList().ForEach(x => WorkFlowViewModel.Templates.Add(x));
    }

    public void Save() {
      File.WriteAllText(TemplatesFileName, JsonSerializer.Serialize(Templates));
    }
  }
}
