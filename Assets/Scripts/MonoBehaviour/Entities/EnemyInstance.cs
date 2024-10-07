using UnityEngine;
using UnityEngine.Events;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] public Character Character;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private StatBlockDisplayer statsDisplayer;
    [SerializeField] private TextNotifier notifier;
    [SerializeField] private Animator animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private EnemyInstanceGameEvent enemySelectionChannel;

    public UnityEvent<EnemyInstance> OnDeath; 

    private bool isSetupDone = false;
    void Start()
    {
        if(Character.enemyData && !isSetupDone) SetupInstance(Character.enemyData); 
    }

    public void SetupInstance(EnemyData setupData)
    {
        Character.enemyData = setupData;
        Character.Initialize();
        statsDisplayer.SetupAbilities(setupData.statBlockInstance.Stats.Stats);

        animatorController.runtimeAnimatorController = setupData.animationController;
        spriteRenderer.sprite = setupData.enemySprite;
        healthBar.maxHealth = healthBar.currentHealth = Character.maxHealth;

        Character.Notify += OnNotify;
        Character.OnHealthModified += healthBar.SetHealth;
        Character.OnDeath += delegate
        {
            OnDeath.Invoke(this);
        };

        isSetupDone = true;
    }

    public void ApplyCard()
    {
                         
    }

    public void HandleSelection()
    {
        if(HandManager.Instance.selectedCard.Value == null) ShowStats(true);
        enemySelectionChannel.Raise(this);

    }

    public void OnDie()
    {
        OnDeath?.Invoke(this);
    }

    public void OnNotify(string s)
    {
        notifier.DisplayNotification(s);
    }

    public void HandleHover()
    {
        // Debug.Log(HandManager.Instance.name);

        if(HandManager.Instance.selectedCard.Value == null)
        {
            ShowStats(false);
            return;
        }

        CardEffect card = HandManager.Instance.selectedCard.Value.CardEffect;
        if(card is AttackEffect)
        {
            healthBar.PreviewDamage(1, card.Dice.Max - 1);
        }
        
    }

    private bool autoFade = false;

    public void ShowStats(bool doautoFade)
    {
        if(autoFade) return;
        autoFade = doautoFade;
        statsDisplayer.gameObject.SetActive(true);
    }
    public void UnshowStats(bool deselection) 
    {
        //if is deselection, turn true the autoFade
        autoFade = !deselection && autoFade;
        if(!autoFade)
            statsDisplayer.gameObject.SetActive(false);
    }
}
