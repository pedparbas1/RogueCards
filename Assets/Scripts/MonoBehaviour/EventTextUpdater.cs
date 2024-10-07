using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventTextUpdater : MonoBehaviour
{
    
    [SerializeField] TMP_Text displayer;
    [SerializeField] bool isDebugging = true;
    
    void OnEnable()
    {
        displayer = displayer != null? displayer : GetComponent<TMP_Text>();
        if (isDebugging) Debug.Log(displayer.name);
    }

    public void UpdateNumber(int n)
    {
        displayer.text = n.ToString();
        if(isDebugging) Debug.Log($"{displayer.name} : {n}");
    }

    public void UpdateText(string s) => displayer.text = s;
}
