using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class DocumentationExport : EditorWindow
{
    public static string exePath = Application.dataPath + "/..\\DocXExporter\\Exe\\DocX Export.exe";
    public static string txtPath = Application.dataPath + "/..\\DocXExporter\\Exe\\documentationRaw.txt";
    public static string docxPath = Application.dataPath + "/..\\DocXExporter\\Exe\\documentation.docx";

    public static List<string> ignoreClassesContains = 
        new List<string> { "UnityEngine.", "UnityEditor.", "TMPro.", "VSCodeDiscovery.", "Packages.", "VSCodeEditor.", "UnityEditorInternal.", "CollabProxy." };

    [MenuItem("Tools/Export Documentation")]
    public static void ExportTranslations()
    {
        CreateFile();

        Process p = new Process();
        p.StartInfo.UseShellExecute = true;
        p.StartInfo.FileName = exePath;

        p.StartInfo.Arguments = txtPath;
        p.Start();

        UnityEngine.Debug.Log("Trying to export DocX file to : " + docxPath);

        Application.OpenURL(Application.dataPath + "/..\\DocXExporter\\Exe\\");
    }

    public static void CreateFile()
    {
        System.IO.File.Create(txtPath).Dispose();

        MonoScript[] scripts = (MonoScript[])UnityEngine.Object.FindObjectsOfTypeIncludingAssets(typeof(MonoScript));

        List<Type> searchedClasses = new List<Type>();
        string output = "";

        foreach (MonoScript m in scripts)
        {
            Type t = m.GetClass();
            if (t != null && t != typeof(DocumentationExport))
            {
                bool ignore = false;
                for(int i=0; i<ignoreClassesContains.Count; i++)
                {
                    if (t.ToString().Contains(ignoreClassesContains[i])) ignore = true;
                }

                if (!ignore)
                {
                    var derived = t;
                    while (derived.BaseType != null)
                    {
                        if (derived.BaseType == typeof(System.Object) || derived.BaseType == typeof(MonoBehaviour)) break;
                        derived = derived.BaseType;
                            
                    };

                    
                    Type[] interfaces = derived.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        for (int i = 0; i < interfaces.Length; i++)
                        {
                            if (!searchedClasses.Contains(interfaces[i]))
                            {
                                searchedClasses.Add(interfaces[i]);
                                output += CreateClassParagraph(interfaces[i]);
                            }
                            
                        }
                    }
                    else
                    {
                        if (!searchedClasses.Contains(derived))
                        {
                            searchedClasses.Add(derived);
                            output += CreateClassParagraph(derived);
                        }
                    }

                    

                }
                    
            }
        }
        File.WriteAllText(txtPath, output);
    }

    public static string CreateClassParagraph(Type t)
    {
        string output = "-PARAGRAPH- " + t.ToString();

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => t.IsAssignableFrom(p)).ToList();

        for (int i=0; i<types.Count; i++)
        {
            if(types[i] != t)
                output += "\n   " + types[i].ToString();
        }

        output += "\n";
        return output;
    }

    
}
