using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour
{
    public Image statImage;
    public TMP_Text statName;
    public TMP_Text statValue;

    public void SetStat(string statName, int value)
    {
        this.statName.text = statName[..3];
        statValue.text = value.ToString();
    } 
}
