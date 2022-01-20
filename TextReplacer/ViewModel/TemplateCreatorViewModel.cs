using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace TextReplacer.ViewModel {
  public class TemplateCreatorViewModel : BaseViewModel {

    public TemplateCreatorViewModel() {
      SetStatusToStandard();
      AddInfo("Info: Der Indikator steuert welche Spalten eingeblendet werden");
    }

    private String _indicatorText;
    public String IndicatorText {
      get => _indicatorText;
      set => SetProperty(ref _indicatorText, value);
    }

    public ObservableCollection<ExpandoObject> ToInsertLabels { get; set; } = new ObservableCollection<ExpandoObject>();

    public override void Start() {
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(IndicatorText)) {
        SetStatusToInfo("Alle Felder müssen ausgefüllt sein");
        return;
      }
      StringBuilder sb = new StringBuilder();
      foreach (var expando in ToInsertLabels) {
        String temp = TemplateText;
        var dict = (IDictionary<String, Object>)expando;
        foreach (var keyValue in dict) {
          temp = temp.Replace(keyValue.Key, keyValue.Value as String);
        }
      }
      ResultText = sb.ToString();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs args) {
      base.OnPropertyChanged(args);
      switch (args.PropertyName) {
        case nameof(TemplateText):
          FillLabelsToInsert();
          break;
        case nameof(IndicatorText):
          FillLabelsToInsert();
          break;
      }
    }

    private void FillLabelsToInsert() {
      if (String.IsNullOrEmpty(TemplateText)
        || String.IsNullOrEmpty(IndicatorText)) {
        return;
      }

      var words = TemplateText.Split(' ');
      List<String> keyWords = words.Where(x => x.StartsWith(IndicatorText))
        .Distinct()
        .OrderBy(x => x)
        .ToList();

      dynamic columns = new ExpandoObject();
      foreach (String key in keyWords) {
        ((IDictionary<String, Object>)columns).Add(key, "");
      }
      ToInsertLabels.Add(columns);
    }
  }
}
