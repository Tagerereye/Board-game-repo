using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item
{
    new public string name;
    public Sprite icon = null;
    public string description;
    public string descriptionShort;
    public int damage = 0, maxhp = 0, mana = 0, spellPower = 0, goldCost = 0;

    public virtual void OnEquip(PlayerScript player)
    {
        player.damage.AddModifier(damage);
        player.maxHealth.AddModifier(maxhp);
        player.currentHealth += maxhp;
        player.mana.AddModifier(mana);
        player.manaCurrent += mana;
        player.spellPower.AddModifier(spellPower);
    }
    public virtual void OnUnequip(PlayerScript player)
    {
        player.damage.RemoveModifier(damage);
        float percentage = (float)player.currentHealth / (float)player.maxHealth.Value();
        player.maxHealth.RemoveModifier(maxhp);
        player.currentHealth = (int)((float)player.maxHealth.Value() * percentage);
        player.mana.RemoveModifier(mana);
        if (player.manaCurrent > player.mana.Value()) player.manaCurrent = player.mana.Value();
        player.spellPower.RemoveModifier(spellPower);
    }
    public virtual void OnUse()
    {
        
    }
}