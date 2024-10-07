using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceInstance : MonoBehaviour
{
    [SerializeField] TMP_Text textContainer;
    [SerializeField] TextNotifier notifier;

    public void Notify(string s) => notifier.DisplayNotification(s);

    public void SetDiceValue(int num)
    {
        textContainer.text = $"{num}";
    }
}
