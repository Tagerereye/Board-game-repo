using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Starter Vest")]
public class StarterVest : Item
{
    public StarterVest()
    {
        description = "+2 HP. +1 Attack Damage. +1 Spell Power.";
        descriptionShort = description;
        icon = Resources.Load<Sprite>("Sprites/StarterVestIcon");
        name = "Starter Vest";
        damage = 1;
        maxhp = 2;
        spellPower = 1;
        goldCost = 10;
    }
}
