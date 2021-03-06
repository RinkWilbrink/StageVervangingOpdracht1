using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class SaveKeybinds : MonoBehaviour
{
    // Variables
    [Header("Input Asset")]
    [SerializeField] private InputActionAsset inputAsset;

    /// <summary>Read and assign the Keybinds stored in the settings file.</summary>
    public void GetKeybinds()
    {
        string[] Keybinds;
        string values = "";

        try {
            values = GetKeybindString();
        } catch {
            CreateAndWriteSettings(string.Empty);
        }

        if(!string.IsNullOrEmpty(values) && !string.IsNullOrWhiteSpace(values))
        {
            string[] ContentStrings = values.Split(';');

            for (int i = 0; i < ContentStrings.Length; i++)
            {
                string[] ActionAndKeybind = ContentStrings[i].Split('=');
                string[] ActionAndIndex = ActionAndKeybind[0].Split('_');

                // Set Keybind data
                InputAction action = inputAsset.FindAction(string.Format("{0}", ActionAndIndex[0]));
                action.ApplyBindingOverride(int.Parse(ActionAndIndex[1]), ActionAndKeybind[1]);
                
                // Cleanup local variables.
                ActionAndKeybind = null;
                ActionAndIndex = null;
                //action = null;
            }
        }
    }

    /// <summary></summary>
    /// <param name="KeyBinds">Array of Keybinds and their action maps! E.G. Player/Move_1 | Player/Jump_0 | UI/Up_0.</param>
    public void SetKeybinds(string[] KeyBinds)
    {
        string settingsText = string.Empty;

        for (int i = 0; i < KeyBinds.Length; i++)
        {
            settingsText += string.Format("{0}", KeyBinds[i]);

            if (i < KeyBinds.Length - 1)
            {
                settingsText += ";";
            }
        }

        // Create or overwrite the settings file in the template folder.
        CreateAndWriteSettings(settingsText);
    }

    /// <summary>Write the currently assigned keys with their action in a file to store the keybinds for next time.</summary>
    /// <param name="Contents"></param>
    public void SetKeybinds(string Contents)
    {
        //Debug.LogFormat("Contents: {0}", Contents);

        // Create or overwrite the settings file in the template folder.
        CreateAndWriteSettings(Contents);
    }

    #region Get/Set Data Functions

    /// <summary>Get a string with all the content of the file where the keybinds are stored.</summary>
    /// <returns></returns>
    public string GetKeybindString()
    {
        return Regex.Replace(System.IO.File.ReadAllText(string.Format("{0}/binds.txt", Application.persistentDataPath)), @"\t|\n|\r", string.Empty);
    }

    /// <summary>Create and/or write to the file that stores all keybinds.</summary>
    /// <param name="WriteText"></param>
    private void CreateAndWriteSettings(string WriteText)
    {
        if (!System.IO.File.Exists(string.Format("{0}/binds.txt", Application.persistentDataPath)))
        {
            System.IO.FileStream banaan = System.IO.File.Create(string.Format("{0}/binds.txt", Application.persistentDataPath));
            banaan.Close();
        }

        using (var writer = new System.IO.StreamWriter(string.Format("{0}/binds.txt", Application.persistentDataPath)))
        {
            // Write the text given as parameter and put that text in to the newly created file.
            writer.WriteLine(WriteText);
        }

        // Cleanup local variables
        WriteText = null;
    }

    #endregion
}