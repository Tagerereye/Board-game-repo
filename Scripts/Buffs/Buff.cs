using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public bool fightDuration;
    public bool turnDuration;
    public bool battleDuration;
    public int duration;

    public void AddBuffToPlayer(PlayerScript player)
    {
        player.AddBuff(this);
    }
    public void RemoveBuffFromPlayer(PlayerScript player)
    {
        player.RemoveBuff(this);
    }

    public virtual void Effect(PlayerScript player)
    {

    }
    public virtual void OnAdd(PlayerScript player)
    {

    }
    public virtual void OnRemove(PlayerScript player)
    {

    }
}