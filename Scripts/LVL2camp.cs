using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL2camp : LVLcamp
{
    // Start is called before the first frame update
    public override void StartFunction()
    {
        base.StartFunction();
        level = 2;
        campName = "Spiders";
        health = 8;
        damage = 3;
        gold = 20;
        sprite = Resources.Load<Sprite>("Sprites/goblin");
        expYield = 2;
    }
    public override void CampAction()
    {
        BattleScreenScript.instance.leftPlayer.ChangeHealth(-damage);
        BattleScreenScript.combatLog.text = "Enemy attacks. Deals "+damage+ " damage.";
        base.CampAction();
    }

}
