using UnityEngine;

[CreateAssetMenu(fileName = "new CardInstanceVariable", menuName ="Variables/new CardInstance")]
public class CardInstanceVariable : GlobalVariable<CardInstance>
{
    void OnDisable()
    {
        Value = null;
    }
}