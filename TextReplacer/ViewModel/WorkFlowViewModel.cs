using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TextReplacer.Binding;
using TextReplacer.Control;
using TextReplacer.Dto;

namespace TextReplacer.ViewModel {
  public class WorkFlowViewModel : BaseViewModel {

    private ICommand _addTextReplacerCommand;
    public ICommand AddTextReplacerCommand {
      get {
        if (_addTextReplacerCommand == null) {
          _addTextReplacerCommand = new RelayCommand(
              param => AddTextReplacer()
          );
        }
        return _addTextReplacerCommand;
      }
    }

    private ICommand _addTemplateCreatorCommand;
    public ICommand AddTemplateCreatorCommand {
      get {
        if (_addTemplateCreatorCommand == null) {
          _addTemplateCreatorCommand = new RelayCommand(
              param => AddTemplateCreator()
          );
        }
        return _addTemplateCreatorCommand;
      }
    }

    private ICommand _addRgExCommand;
    public ICommand AddRgExCommand {
      get {
        if (_addRgExCommand == null) {
          _addRgExCommand = new RelayCommand(
              param => AddRgEx()
          );
        }
        return _addRgExCommand;
      }
    }

    private String _infoTemplateText = "Info: Bei {x} wird der Output des letzten Schritt eingesetzt";


    private ObservableCollection<TabItemElement> _tabItems = new ObservableCollection<TabItemElement>();
    public ObservableCollection<TabItemElement> TabItems {
      get => _tabItems;
      set => SetProperty(ref _tabItems, value);
    }

    public WorkFlowViewModel() {
      _tabItems.Add(new TabItemElement(Enum.ReplacerType.None, this,new WorkFlowAddControl()));
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    public override void Start() {
      StringBuilder sb = new StringBuilder();
      ResultText = sb.ToString();
    }

    public void AddTextReplacer() {
      var t = new TabItemElement(
        Enum.ReplacerType.TextReplacer, 
        new TextReplacerControlViewModel(), 
        new TextReplacerControl()
      );
      TabItems.Insert(0, t);
    }

    public void AddTemplateCreator() {
      var t = new TabItemElement(
        Enum.ReplacerType.TemplateCreator, 
        new TemplateCreatorControlViewModel(), 
        new TemplateCreatorControl()
      );
      TabItems.Insert(0, t);
    }

    public void AddRgEx() {
      var t = new TabItemElement(
        Enum.ReplacerType.RgEx, 
        new RgExControlViewModel(), 
        new RgExControl());
      TabItems.Insert(0, t);
    }

    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }

  }
}
