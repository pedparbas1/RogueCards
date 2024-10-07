using System;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public int maxHealth, currentHealth;
    [SerializeField] BarFiller currentHealthBar, previewDamage, calculatedHealth;

    private bool isDirty;
    private float currentHealthValue, currentPreviewDamageValue, currentCalculatedHealthValue;

    void Start()
    {
        BarFiller[] fillers = GetComponentsInChildren<BarFiller>();
        // foreach(BarFiller filler in fillers)
        // {
        //     filler.SetFull();
        // } 

        if(currentHealthBar == null) currentHealthBar = fillers[0];
        if(previewDamage == null) previewDamage = fillers[1];
        if(calculatedHealth == null) calculatedHealth = fillers[2];

    }

    public void SetHealth(int hp)
    {
        currentHealth = hp;
        currentHealthBar.SetFillAmount(maxHealth, currentHealth);
        previewDamage.SetFillAmount(maxHealth, currentHealth);
        calculatedHealth.SetFillAmount(maxHealth, currentHealth);

    }
    
    public void TakeHeal(int amount)
    {

    }
    

    public void PreviewDamage(int minDamage, int maxDamage)
    {
        isDirty = true;
        currentPreviewDamageValue =
            previewDamage.SetFillAmount(maxHealth, currentHealth - minDamage);
        currentCalculatedHealthValue = 
            calculatedHealth.SetFillAmount(maxHealth, currentHealth - maxDamage);
        Debug.Log($"MinDmg: {currentHealth - minDamage}, MaxDmg: {currentHealth - maxDamage}");
    }

    public void UnpreviewDamage()
    {
        if(!isDirty) return;
        
        currentHealthBar.SetFillAmount(maxHealth, currentHealth);
        previewDamage.SetFillAmount(maxHealth, currentHealth);
        calculatedHealth.SetFillAmount(maxHealth, currentHealth);
        isDirty = false;
    }

}