using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicStrike : Spell
{
    public HeroicStrike()
    {
        name = "Heroic Strike";
        manaCost = 0;
        learnCost = 1;
    }
    public override void Effect()
    {
        base.Effect();
        BattleScreenScript.combatLog.text = caster.gamename + " heroically strikes. ";
        if (isAgainstAI)
        {
            caster.ApplyDamage(2 + caster.damage.Value());
        }
        
    }
}
