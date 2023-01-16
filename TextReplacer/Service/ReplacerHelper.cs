using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextReplacer.Enum;

namespace TextReplacer.Service {
  public class ReplacerHelper {

    public static String ReplaceWorkflow(String template, String input) {
      Dictionary<String, String> replacements = new Dictionary<string, string>();
      replacements.Add("{x}", input);
      var regex = GetReplacerRegex(replacements);
      return GetReplacedText(template, replacements, regex);
    }

    /// <summary>Replace the variables with the replacements.</summary>
    /// <param name="template"></param>
    /// <param name="replacements"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static String GetReplacedText(String template, Dictionary<String, String> replacements, Regex regex) {
      template = regex.Replace(template, m => {
        if (!replacements.ContainsKey(m.Value)) {
          return "";
        }
        return replacements[m.Value];
      });
      return template;
    }

    /// <summary>Gets the replacer reges for replacements.</summary>
    /// <param name="replacements"></param>
    /// <returns></returns>
    public static Regex GetReplacerRegex(Dictionary<String, String> replacements) {
      return new Regex(String.Join("|", replacements.Keys.Select(k => Regex.Escape(k))));
    }

    /// <summary>Gets the replacements for the given variables and their values.</summary>
    /// <param name="variables"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Dictionary<String, String> GetReplacements(String[] variables, String[] values) {
      Dictionary<String, String> replacements = new Dictionary<String, String>();
      for (Int32 i = 0; i < variables.Length; i++) {
        if (values.Length < i) {
          continue;
        }

        String variable = variables[i];
        String value = values[i];
        replacements.Add(variable, value);
        replacements.Add("*" + variable, Char.ToUpper(value[0]) + value.Substring(1));
        replacements.Add("_" + variable, Char.ToLower(value[0]) + value.Substring(1));
      }

      return replacements;
    }

    public static String GetSeperator(String splitCharsForLabels) {
      String seperator = splitCharsForLabels;
      // split the labels on the seperator
      if (seperator == "\\r\\n") {
        seperator = Environment.NewLine;
      }
      return seperator;
    }

    public static Boolean CreateFile(Boolean overrideExistingFiles, String fileName, String content) {
      if (File.Exists(fileName) && !overrideExistingFiles) {
        return false;
      }
      try {
        File.WriteAllText(fileName, content);
      }
      catch (Exception) {
        return false;
      }
      return true;
    }

    /// <summary>Applies the output settings to the given values.</summary>
    /// <param name="sb">The text to write down.</param>
    /// <param name="filePath">The path for file.</param>
    /// <returns>The string to add in the result.</returns>
    public static String ApplyOutputSettings(StringBuilder sb, Dictionary<String, String> replacements, Regex regex,
      OutputType outputType,
      String outputFileName,
      String outputFolderPath,
      Boolean? outputOverrideExistingFiles) 
    {
      switch (outputType) {
        case OutputType.WithNewLines:
          sb.AppendLine();
          return sb.ToString();
        case OutputType.InFiles:
          var fileName = ReplacerHelper.GetReplacedText(outputFileName, replacements, regex);
          String filePath = Path.Combine(outputFolderPath, fileName);

          Boolean fileCreated = CreateFile(outputOverrideExistingFiles ?? false, filePath, sb.ToString());
          String prefix = fileCreated ? "Erstellt:" : "Nicht erstellt:";
          sb.Clear();
          sb.AppendLine(prefix + " " + filePath);
          return sb.ToString();
        default:
          return sb.ToString();
      }
    }

    public static String[] GetSplittedLabels(String toInsertLabels, String seperator) {
      return toInsertLabels.Split(new String[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
    }
  }
}
