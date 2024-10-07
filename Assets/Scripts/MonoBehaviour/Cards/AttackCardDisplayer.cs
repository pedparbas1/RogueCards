using TMPro;
using UnityEngine;

[System.Serializable]
public class AttackCardDisplayer : CardDisplayer<AttackEffect>, IDisplayer
{
    [SerializeField] private TMP_Text damageTypeContainer;

    public void Display()
    {
        UpdateCardDisplayer();
    }

    public override void UpdateCardDisplayer()
    {
        base.UpdateCardDisplayer();
        // Debug.Log(damageTypeContainer.text);

        // Debug.Log(effect.EffectType.effectName);
        damageTypeContainer.text = effect.EffectType.effectName;
    }
}
