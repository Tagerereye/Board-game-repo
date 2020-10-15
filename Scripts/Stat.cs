using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int baseValue;
    public List<int> modifiers = new List<int>();
    public int Value()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int value)
    {
        if (value != 0) modifiers.Add(value);
    }
    public void RemoveModifier(int value)
    {
        if (value != 0) modifiers.Remove(value);
    }
}
