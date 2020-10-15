using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomeOfTheAncients : Item
{
    public TomeOfTheAncients()
    {
        description = "Gives your character 2 experience points. Consumed on use.";
        descriptionShort = "Gain 4 exp.";
        name = "Tome of the Ancients";
        icon = Resources.Load<Sprite>("Sprites/TomeOfTheAncientsIcon");
        goldCost = 10;
    }
    public override void OnUse()
    {
        PlayerScript player = BattleSys.instance.players[BattleSys.instance.currentTurn];
        player.GainExp(2);
        player.RemoveItem(this);
    }
}
