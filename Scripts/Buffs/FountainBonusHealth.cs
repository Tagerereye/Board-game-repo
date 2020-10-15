using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBonusHealth : Buff
{
        public FountainBonusHealth()
    {
        fightDuration = false;
        turnDuration = false;
        battleDuration = true;
        duration = 1;
    }
    public override void OnAdd(PlayerScript player)
    {
        player.maxHealth.modifiers.Add(2);
        player.currentHealth += 2;
    }
    public override void OnRemove(PlayerScript player)
    {
        player.maxHealth.modifiers.Remove(2);
        if (player.currentHealth > player.maxHealth.Value()) player.currentHealth = player.maxHealth.Value();
    }
}
