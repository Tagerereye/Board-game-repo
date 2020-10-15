using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicStrike : Spell
{
    public HeroicStrike()
    {
        name = "Heroic Strike";
        manaCost = 0;
    }
    public override void Effect()
    {
        base.Effect();
        if(isAgainstAI)
        {
            caster.ApplyDamage(2 + caster.damage.Value());
        }
        
    }
}
