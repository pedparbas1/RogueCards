using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New AttackEffect", menuName = "Cards/AttackEffect")]
public class AttackEffect : ScriptableObject, ICardEffect
{
    [SerializeField] public CardEffect weapon;
    public void UseEffect()
    {

    }
}