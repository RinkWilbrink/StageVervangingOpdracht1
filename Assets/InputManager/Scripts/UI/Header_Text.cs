using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header_Text : MonoBehaviour
{
    // Variables
    [SerializeField] private string HeaderText;

    private void OnValidate()
    {
        // Sets the Text for the header so the user doesnt need to go into a child object.
        transform.Find("Header Text").GetComponent<TMPro.TextMeshProUGUI>().text = HeaderText;
    }
}
