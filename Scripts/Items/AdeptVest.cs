using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdeptVest : Item
{
    public AdeptVest()
    {
        description = "+4 HP. +2 Attack Damage. +2 Spell Power.";
        descriptionShort = description;
        icon = Resources.Load<Sprite>("Sprites/AdeptVestIcon");
        name = "Adept Vest";
        damage = 2;
        maxhp = 4;
        spellPower = 2;
        goldCost = 20;
    }
}
