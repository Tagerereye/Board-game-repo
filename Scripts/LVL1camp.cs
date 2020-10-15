using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1camp : LVLcamp
{
    // Start is called before the first frame update
    public override void StartFunction()
    {
        base.StartFunction();
        level = 1;
        campName = "Goblins";
        health = 6;
        damage = 2;
        gold = 10;
        sprite = Resources.Load<Sprite>("Sprites/goblin");
        expYield = 1;
    }
    public override void CampAction()
    {
        BattleScreenScript.instance.leftPlayer.ChangeHealth(-damage);
        BattleScreenScript.combatLog.text = "Enemy attacks. Deals "+damage+ " damage.";
        base.CampAction();
    }

}
