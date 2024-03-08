using System.Collections.Generic;
using TextReplacer.ViewModel;

namespace TextReplacer.Dto {

  /// <summary>Dto to save all the templates to.</summary>
  public class TemplateSaveDto {

    public List<TextReplacerControlViewModel> TextReplacerTemplates { get; set; } = new List<TextReplacerControlViewModel>();
    public List<TemplateCreatorControlViewModel> TemplateCreatorTemplates { get; set; } = new List<TemplateCreatorControlViewModel>();
    public List<RgExControlViewModel> RgExTemplates { get; set; } = new List<RgExControlViewModel>();
    public List<WorkFlowTemplateSaveDto> WorkFlowTemplates { get; set; } = new List<WorkFlowTemplateSaveDto>();

  }
}
