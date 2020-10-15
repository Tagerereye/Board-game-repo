using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSalve : Item
{
    public HealingSalve()
    {
        description = "Restores 4 health to the player. Consumed on use.";
        descriptionShort = "Drink to restore 4hp.";
        icon = Resources.Load<Sprite>("Sprites/HealingSalveIcon");
        name = "Healing Salve";
        goldCost = 10;
    }
    //public override void OnUnequip(PlayerScript player){}
    public override void OnUse()
    {
        PlayerScript player = BattleSys.instance.players[BattleSys.instance.currentTurn];
        player.ChangeHealth(4);
        player.RemoveItem(this);
    }
}
