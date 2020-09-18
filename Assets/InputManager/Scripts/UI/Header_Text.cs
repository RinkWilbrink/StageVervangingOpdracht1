using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header_Text : MonoBehaviour
{
    // Variables
    [SerializeField] private string HeaderText;

    private void OnValidate()
    {
        transform.Find("Header Text").GetComponent<TMPro.TextMeshProUGUI>().text = HeaderText;
    }
}
