using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdeptBreastplate : Item
{
    public AdeptBreastplate()
    {
        description = "+8 Health.";
        descriptionShort = description;
        icon = Resources.Load<Sprite>("Sprites/AdeptBreastplateIcon");
        name = "Adept Breastplate";
        damage = 0;
        maxhp = 8;
        spellPower = 0;
        goldCost = 20;
    }
}
