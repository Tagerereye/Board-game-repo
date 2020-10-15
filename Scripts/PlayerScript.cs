using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public delegate void AdditionalActions(PlayerScript player);
    public AdditionalActions AttackActions, FightTurnActions, BattleEndActions,TurnEndActions;
    public int boardPosition;
    public Stat maxHealth;
    public int currentHealth;
    public Stat damage;
    public Stat mana;
    public int manaCurrent;
    public Stat spellPower;
    public int gold;

    public int experience;
    public int level;
    public int[] lvlupReq = { 0, 2, 4, 8, 16 };

    public Sprite texture;
    public string playerClass;
    public string gamename;
    BattleScreenScript battlescreen;

    public List<Buff> buffs = new List<Buff>();
    public List<Item> inventory = new List<Item>();
    public List<Spell> learnedSpells = new List<Spell>();
    public static int inventorySpace = 6;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = texture;
        currentHealth = maxHealth.Value();
        manaCurrent = mana.Value();
        battlescreen = BattleScreenScript.instance;
        level = 1;
        experience = 0;
        inventory.Capacity = inventorySpace;
    }
    // Update is called once per frame
    public void SquareActions(int prev, int current, GameObject pos)
    {
        for(int i=prev;i<=current;i++)
        {
            var c = pos.transform.GetChild(i);
            if(c) Debug.Log(c);
        }

    }
    void Action()
    {

    }
    public virtual void Attack()
    {
        BattleScreenScript.combatLog.text = gamename + " attacks. ";
        ApplyDamage(damage.Value());

        if(AttackActions!=null) AttackActions.Invoke(this);
    }
    public void ApplyDamage(int value)
    {
        if (battlescreen.isAgainstAI) battlescreen.enemyCamp.ChangeHealth(-value);
        BattleScreenScript.combatLog.text += "Deals " + value + " damage.";
    }
    public void ChangeHealth(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth.Value()) currentHealth = maxHealth.Value();
        if (currentHealth < 0) currentHealth = 0;
    }
    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        AttackActions += buff.Effect;
        buff.OnAdd(this);
    }
    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
        AttackActions -= buff.Effect;
        buff.OnRemove(this);
    }
    
    public void BuffUpdateFight()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            Buff b = buffs[i];
            if (b.fightDuration == true) b.duration--;
            if (b.duration == 0) { RemoveBuff(b); i--; }
        }
        if (FightTurnActions != null) FightTurnActions.Invoke(this);
    }
    public void BuffUpdateTurn()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            Buff b = buffs[i];
            if (b.turnDuration == true) b.duration--;
            if (b.duration == 0) { RemoveBuff(b); i--; }
        }
        if (TurnEndActions != null) TurnEndActions.Invoke(this);
    }
    public void BuffUpdateBattle()
    {
        for(int i=0;i<buffs.Count;i++)
        {
            Buff b = buffs[i];
            if (b.battleDuration == true) b.duration--;
            if (b.duration == 0) { RemoveBuff(b); i--; }
        }
        if (BattleEndActions != null) BattleEndActions.Invoke(this);
    }
    public void GainExp(int value)
    {
        experience += value;
        if(experience >= lvlupReq[level])
        {
            experience -= lvlupReq[level];
            level++;
        }
    }
    public void AddItem(Item item)
    {
        if (inventory.Count < inventorySpace)
        {
            inventory.Add(item);
        item.OnEquip(this);
        BattleSys.instance.onUpdatingUICallBack.Invoke();
        if(BattleSys.instance.onInventoryChangeCallback!=null) BattleSys.instance.onInventoryChangeCallback.Invoke(item);
        }
    }
    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        item.OnUnequip(this);
        BattleSys.instance.onUpdatingUICallBack.Invoke();
        if (BattleSys.instance.onInventoryChangeCallback != null) BattleSys.instance.onInventoryChangeCallback.Invoke(item);
    }
    public void LearnSpell(Spell spell)
    {
        learnedSpells.Add(spell);
    }
    public void UnLearnSpell(Spell spell)
    {
        learnedSpells.Remove(spell);
    }
}
