using System.Collections.Generic;
using System.Windows.Input;
using TextReplacer.Binding;
using TextReplacer.Control;
using TextReplacer.Dto;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class WorkFlowControlViewModel : BindableBaseStartReplace {

    private readonly WorkFlowViewModel _viewModel;
    public WorkFlowControlViewModel(WorkFlowViewModel workFlow) {     
      _viewModel = workFlow;
    }

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
    public void AddTextReplacer() {
      var t = new TabItemElement(
        Enum.ReplacerType.TextReplacer,
        new TextReplacerControlViewModel(),
        new TextReplacerControl()
      );
      _viewModel.TabItems.Insert(0, t);
    }

    public void AddTemplateCreator() {
      var t = new TabItemElement(
        Enum.ReplacerType.TemplateCreator,
        new TemplateCreatorControlViewModel(),
        new TemplateCreatorControl()
      );
      _viewModel.TabItems.Insert(0, t);
    }

    public void AddRgEx() {
      var t = new TabItemElement(
        Enum.ReplacerType.RgEx,
        new RgExControlViewModel(),
        new RgExControl());
      _viewModel.TabItems.Insert(0, t);
    }


    public List<BindableBaseStartReplace> TabItems { get; set; }
  }
}
