﻿using System.Windows.Controls;

namespace TextReplacer {
  /// <summary>
  /// Interaction logic for TemplateCreatorView.xaml
  /// </summary>
  public partial class TemplateCreatorView : UserControl {
    public TemplateCreatorView() {
      InitializeComponent();

      Variables.ToolTip = @"Hier können mehrere Variablen angegeben werden,
Die Reihenfolge muss dabei gleich wie im Vorgabetext gehalten werden,
Bitte Trennzeichen beachten.";
      SplitChar.ToolTip = @"Beinhaltet das Trennzeichen, zum Trennen von Variablen und Labels";
    }
  }
}
