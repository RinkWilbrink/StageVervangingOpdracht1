using UnityEngine;
using System.Text.RegularExpressions;

public class SaveKeybinds : MonoBehaviour
{
    // Variables

    // Application.persistentDataPath

    public void GetKeybinds()
    {
        string[] Keybinds;

        //CheckFilePathExistance(settings_Path);
        string values = Regex.Replace(GetContentFromFileAtPath(string.Format("{0}/settings.txt", Application.persistentDataPath)), @"\t|\n|\r", string.Empty);

        string[] ContentStrings = values.Split(';');

        for (int i = 0; i < ContentStrings.Length; i++)
        {
            string[] CurrentValue = ContentStrings[i].Split('=');

            if (CurrentValue[0] == "ScriptPath")
            {
                //scripts_Path = CurrentValue[1];
            }
            //switch
        }
    }

    public void SetKeybinds(string[] KeyBinds)
    {
        string settingsText = string.Format("ScriptPath={0}", Application.persistentDataPath);

        for (int i = 0; i < KeyBinds.Length; i++)
        {
            string KeybindDivider = ";";

            if(i >= KeyBinds.Length - 1)
            {
                KeybindDivider = string.Empty;
            }

            settingsText += string.Format("{0}{1}", KeyBinds[i], KeybindDivider);
        }

        // Create or overwrite the settings file in the template folder.
        CreateAndWriteFile(string.Format("{0}/binds.txt", Application.persistentDataPath), settingsText);
    }

    private string GetContentFromFileAtPath(string FilePath)
    {
        return System.IO.File.ReadAllText(FilePath);
    }

    private void CreateAndWriteFile(string NewFilePath, string WriteText)
    {
        using (var writer = new System.IO.StreamWriter(NewFilePath))
        {
            // Write the text given as parameter and put that text in to the newly created file.
            writer.WriteLine(WriteText);
        }

        // Cleanup local variables
        NewFilePath = null;
        WriteText = null;
    }
}