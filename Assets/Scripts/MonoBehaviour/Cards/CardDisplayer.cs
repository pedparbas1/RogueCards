using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardDisplayer<T> : MonoBehaviour where T : CardEffect 
{
    [SerializeField] GameObjectVariable cardPrefab;
    
    [SerializeField] private CardInstance cardInstance;
    public void SetCardStats(CardInstance cardInstance)
    {
        this.cardInstance = cardInstance;
        UpdateCardDisplayer();
    }

    //Display Targets
    [SerializeField] private TMP_Text cardNameContainer;
    [SerializeField] private TMP_Text staminaCostContainer;
    [SerializeField] private Image iconContainer;
    [SerializeField] private GameObject diceContainer;
    [SerializeField] private TMP_Text abilityScoreContainer;

    void OnEnable() 
    {
        cardInstance.OnChangeEvent += SetCardStats;
    }
    
    protected T effect;

    public virtual void UpdateCardDisplayer()
    {
        this.effect = (T) cardInstance.CardEffect;
        cardNameContainer.text = effect.CardName;
        staminaCostContainer.text = effect.StaminaCost.ToString();
        iconContainer.sprite = effect.Weapon;
        _ = Object.Instantiate(effect.Dice.dicePF, diceContainer.transform);
        abilityScoreContainer.text = effect.AbilityScore.ToString();
    }
}