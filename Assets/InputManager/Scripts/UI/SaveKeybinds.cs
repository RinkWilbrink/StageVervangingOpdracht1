using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class SaveKeybinds : MonoBehaviour
{
    // Variables
    [Header("Input Asset")]
    [SerializeField] private InputActionAsset inputAsset;

    // Application.persistentDataPath

    //ApplyKeybindOverride to set the keys

    public void GetKeybinds()
    {
        string[] Keybinds;

        //CheckFilePathExistance(settings_Path);
        string values = Regex.Replace(GetContentFromFileAtPath(string.Format("{0}/binds.txt", Application.persistentDataPath)), @"\t|\n|\r", string.Empty);

        Debug.Log(values);

        string[] ContentStrings = values.Split(';');

        for (int i = 0; i < ContentStrings.Length; i++)
        {
            string[] ActionAndKeybind = ContentStrings[i].Split('=');
            string[] ActionAndIndex = ActionAndKeybind[0].Split('_');

            Debug.LogFormat("{0} | {1} | {2} | {3}", ActionAndKeybind[0], ActionAndKeybind[1], ActionAndIndex[0], ActionAndIndex[1]);

            // Set Keybind data
            InputAction action = inputAsset.FindAction(string.Format("{0}", ActionAndIndex[0]));
            action.ApplyBindingOverride(int.Parse(ActionAndIndex[1]), ActionAndKeybind[1]);

            // Cleanup local variables.
            ActionAndKeybind = null;
            ActionAndIndex = null;
            action = null;
        }
    }

    /// <summary></summary>
    /// <param name="KeyBinds">Array of Keybinds and their action maps! E.G. Player/Move_1 | Player/Jump_0 | UI/Up_0.</param>
    public void SetKeybinds(string[] KeyBinds)
    {
        string settingsText = string.Empty;

        for (int i = 0; i < KeyBinds.Length; i++)
        {
            string KeybindDivider = ";";

            if (i >= KeyBinds.Length - 1)
            {
                KeybindDivider = string.Empty;
            }

            settingsText += string.Format("{0}{1}", KeyBinds[i], KeybindDivider);
        }

        // Create or overwrite the settings file in the template folder.
        CreateAndWriteFile(string.Format("{0}/binds.txt", Application.persistentDataPath), settingsText);
    }

    #region Get/Set Data Functions
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
    #endregion
}