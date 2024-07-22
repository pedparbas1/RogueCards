using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private CardEffect cardEffect;
    public void SetCardStats(CardEffect cardEffect)
    {
        this.cardEffect = cardEffect;
        UpdateCardDisplayer();
    }

    //Display Targets
    [SerializeField] private TMP_Text cardNameContainer;
    [SerializeField] private TMP_Text staminaCostContainer;
    [SerializeField] private Image iconContainer;
    [SerializeField] private GameObject diceContainer;
    [SerializeField] private TMP_Text abilityScoreContainer;
    [SerializeField] private TMP_Text damageTypeContainer;

    private void Start()
    {
        UpdateCardDisplayer();
    }

    public void UpdateCardDisplayer()
    {
        Effect e = cardEffect.Effect;
        cardNameContainer.text = e.CardName;
        staminaCostContainer.text = e.StaminaCost.ToString();
        iconContainer.sprite = e.Weapon;
        _ = Instantiate(e.Dice.dicePF, diceContainer.transform);
        abilityScoreContainer.text = e.AbilityScore.ToString();
        damageTypeContainer.text = e.DamageType.damageName;
    }
}