using System;
using System.Collections;
using System.Threading.Tasks;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private SerializedDict<Dice, DiceInstance> previousDices;
    [SerializeField] private DiceInstance dice;
    bool isThrowing = false;

    public async Task<int> ThrowDice(Dice dice, bool isExplosive)
    {
        return  isExplosive ? await ThrowExplodingDice(dice) :
                              await ThrowSingleDice(dice);
        
    }

    private async Task<int> ThrowSingleDice(Dice dice)
    {
        ActivateOrCreateDice(dice);
        int result = dice.ThrowDie();
        await ThrowingSequence(result, dice.Max);
        return result;
    }

    private async Task<int> ThrowExplodingDice(Dice dice)
    {
        int res = await ThrowSingleDice(dice);
        if(res == dice.Max - 1) 
        {
            // this.dice.Notify("Explode!!");
            return res + await ThrowExplodingDice(dice);
        }
        return res;
    }

    private void ActivateOrCreateDice(Dice dice)
    {
        if(previousDices.ContainsKey(dice))
        {
            this.dice = previousDices[dice];
            this.dice.gameObject.SetActive(true);
        } else {
            this.dice = Instantiate(dice.dicePF, transform)
            .GetComponent<DiceInstance>();
            previousDices.Add(dice, this.dice);
        }
    }

    public async Task ThrowingSequence(int res, int max)
    {
        isThrowing = true;
        int i = 0;
        while(isThrowing && i < 100)
        {
            dice.SetDiceValue(i % max);
            i++;
            await Task.Yield();
        } 

        dice.SetDiceValue(res);
        if(res == max - 1) dice.Notify("Exploded!!");

        await Task.Delay((int)(3.5f * 1000));

        dice.gameObject.SetActive(false);
        dice = null;
        isThrowing = false;
    }
}
