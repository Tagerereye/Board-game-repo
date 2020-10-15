using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdeptHatchet : Item
{
    public AdeptHatchet()
    {
        description = "+4 Attack Damage.";
        descriptionShort = description;
        icon = Resources.Load<Sprite>("Sprites/AdeptHatchetIcon");
        name = "Adept Hatchet";
        damage = 4;
        maxhp = 0;
        spellPower = 0;
        goldCost = 20;
    }
}
