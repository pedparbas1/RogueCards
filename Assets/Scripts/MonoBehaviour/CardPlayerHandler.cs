using System;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayerHandler : MonoBehaviour
{
    [SerializeField] public CardInstanceVariable playingCard;
    [SerializeField] public EnemyInstanceVariable enemySelected;
    [SerializeField] public EnemyInstanceVariable heroPlaying;

    [SerializeField] public DiceThrower diceThrower;
    
    [SerializeField] AbilitieScoreType staminaDependentAbilitie;
    [SerializeField] int totalStamina = 3;
    [SerializeField] int currentStamina;

    [SerializeField] StringEvent OnErrorOcurred;
    [SerializeField] IntGameEvent OnChangeStamina;

    private Queue<CardInstance> queuedCards;
    private bool isPlayingCard;
    private bool isPlayingBlocked;

    void Awake()
    {
        diceThrower = diceThrower? diceThrower: GetComponentInChildren<DiceThrower>();
        queuedCards = new();
    }
    
    void Start()
    {
        // Debug.Log("hero CON mod: " + heroPlaying.Value.Character.Stats.Stats.GetModifier(staminaDependentAbilitie).ToString());
        totalStamina = Math.Max(1, 
        totalStamina + heroPlaying.Value.Character.Stats.Stats.GetModifier(staminaDependentAbilitie));
        OnNewRound();
        //Initialize stamina displayer with current stamina
        HandManager.Instance.OnCardPlayedStamina.Raise(currentStamina);
    }

    public void OnNewRound()
    {
        currentStamina = totalStamina;
        OnChangeStamina.Raise(currentStamina);

    }
    public void OnError(string s) 
    {
        OnErrorOcurred?.Raise(s);
        Reset();
    }

    public void AddStamina(int stamina)
    {
        currentStamina += stamina;
        HandManager.Instance.OnCardPlayedStamina.Raise(currentStamina);
    }
    
    public void TryPlayCard()
    {
        CardInstance playingCard = this.playingCard.Value;
        if(playingCard == null)
        {
            Debug.Log("No selected Card");
            return;
        } 

        if(playingCard.CardEffect.StaminaCost > currentStamina)
        {
            OnErrorOcurred?.Raise("Not enough stamina"); 
            return;
        }

        AddStamina(-playingCard.CardEffect.StaminaCost);
        playingCard.Enqueue();
        if (isPlayingCard)
        {
            queuedCards.Enqueue(playingCard);
            Debug.Log($"Queuing: {playingCard.CardEffect.CardName}");
            return;
        }

        TryPlayCard(playingCard);
    }

    public async void TryPlayCard(CardInstance playingCard)
    {
        if(isPlayingBlocked) return;
        isPlayingCard = true;

        if (! await playingCard.CardEffect.PlayEffectAsync(this))
        {
            HardReset(playingCard.CardEffect.StaminaCost);
            return;
        }
        
        CardPlayed(playingCard);
    }

    private void HardReset(int stamina)
    {
        AddStamina(stamina);
        Reset();
    }

    private void Reset()
    {
        foreach(CardInstance card in queuedCards) card.Dequeue();
        queuedCards.Clear();

        isPlayingCard = false;
    }

    private void CardPlayed(CardInstance playedCard)
    {
        // Debug.Log("Card has been played");
        // if(HandManager.Instance != null)
        //     Debug.Log($"HandManager: {HandManager.Instance.name}, Playingcard: {playedCard.CardEffect.CardName}");
        playedCard.Dequeue();
        HandManager.Instance.DiscardCard(playedCard);

        isPlayingCard = false;
        if (!queuedCards.TryDequeue(out CardInstance cardToBePlayed)) 
            return;

        Debug.Log($"Card dequeued: {cardToBePlayed.CardEffect.CardName}");
        if(cardToBePlayed != null)
        {
            TryPlayCard(cardToBePlayed);
            return;
        }
        Debug.Log("Nothing to play");
        //After everything has been played correctly
        Reset();
    }

    public void PlayAttackOnEnemySelection(EnemyInstance target)
    {
        if(target == heroPlaying.Value) return;
        if(playingCard.Value == null && playingCard.Value.CardEffect is not AttackEffect) return;
        enemySelected.SetValue(target);
        TryPlayCard();
    }
}
