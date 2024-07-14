using UnityEngine;
[CreateAssetMenu(menuName = "Card Stats/Create Base CardStats")]
public class CardStats : ScriptableObject
{
    [SerializeField] private string _cardName;
    [SerializeField] private int _staminaCost = 0;
    [SerializeField] private CardEffect[] effects = new CardEffect[3];
    [SerializeField] private AbilitieScoreType cardMainAbility;

}