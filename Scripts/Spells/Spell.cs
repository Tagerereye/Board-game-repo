using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string name;
    public int manaCost;
    public PlayerScript caster, enemy;
    public LVLcamp enemyAI;
    public bool isAgainstAI;

    public virtual void Effect()
    {
        caster = BattleScreenScript.instance.currentPlayer;
        isAgainstAI = BattleScreenScript.instance.isAgainstAI;
        if (isAgainstAI == false) enemy = BattleScreenScript.instance.currentPlayer;
        else enemyAI = BattleScreenScript.instance.enemyCamp;
    }
}
