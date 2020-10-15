using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBonusAttack : Buff
{
    private void Awake()
    {
        fightDuration = false;
        turnDuration = false;
        battleDuration = false;
        duration = -1;
    }
    public override void Effect(PlayerScript player)
    {
        player.ApplyDamage(player.damage.Value());
        player.RemoveBuff(this);
    }
}
