using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfRegeneration : Item
{
    public RingOfRegeneration()
    {
        description = "At the end of each turn, restore 2 health.";
        descriptionShort = "Restores HP per turn.";
        name = "Ring of Regeneration";
        icon = Resources.Load<Sprite>("Sprites/RingOfRegenerationIcon");
        goldCost = 10;
    }
    public override void OnEquip(PlayerScript player)
    {
        player.TurnEndActions += RingOfRegenerationEffect;
    }
    void RingOfRegenerationEffect(PlayerScript player)
    {
        player.ChangeHealth(2);
        //Debug.Log("REJENERATINN");
    }
    public override void OnUnequip(PlayerScript player)
    {
        player.TurnEndActions -= RingOfRegenerationEffect;
    }
}
