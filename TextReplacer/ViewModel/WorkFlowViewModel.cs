using System;
using System.Collections.ObjectModel;
using System.Linq;
using TextReplacer.Control;
using TextReplacer.Dto;
using TextReplacer.Service;

namespace TextReplacer.ViewModel {
  public class WorkFlowViewModel : BaseViewModel {    

    private String _infoTemplateText = "Info: Bei {x} wird der Output des letzten Schritt eingesetzt";

    private ObservableCollection<TabItemElement> _tabItems = new ObservableCollection<TabItemElement>();
    public ObservableCollection<TabItemElement> TabItems {
      get => _tabItems;
      set => SetProperty(ref _tabItems, value);
    }

    public WorkFlowViewModel(MainViewModel main) : base(main) {
      _tabItems.Add(new TabItemElement(Enum.ReplacerType.None, new WorkFlowControlViewModel(this), new WorkFlowAddControl()));
      SetStatusToStandard();
      AddInfo(_infoTemplateText);
    }

    public override void Start() {
      var tabItems = TabItems.Where(x => x.ReplacerType != Enum.ReplacerType.None)
        .Select(x => x.DataContext)
        .OfType<IStartReplace>()
        .ToList();

      String startInput = "";
      for (int i = 0; i < tabItems.Count; i++) {
        var replacer = tabItems[i];
        if (i == tabItems.Count - 1) { // is last
          startInput = replacer.Start(startInput, ApplyOutputSettings); // apply output settings only for last step
        }
        else {
          startInput = replacer.Start(startInput, (sb,b,c) => sb.ToString());
        }
      }
      ResultText = startInput;
    }

   
    public override void Clear() {
      base.Clear();
      AddInfo(_infoTemplateText);
    }

    public override void Save() {
      var t = new WorkFlowTemplateSaveDto() {
        TabItems = TabItems.Select(x => x.DataContext).ToList()
      };
      _main.Templates.WorkFlowTemplates.Add(t);
      base.AddSave(new WorkFlowControlViewModel(this) { TabItems = t.TabItems });
    }

  }
}
