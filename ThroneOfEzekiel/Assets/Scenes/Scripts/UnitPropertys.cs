using UnityEngine;
using System;

public class UnitPropertys : Singleton<UnitPropertys>
{
    public enum Alignment
    {
        Dark,
        Radiant
    }

    public enum Unit_Class
    {
        Magic,
        Sword,
        Lance,
        Bow,
        Ruler
    }

    public enum Unit_Modifier
    {
        Trident,
        Flying,
        Swift,
        Elusive
    }

    public enum Unit_Family
    {
        Witch,
        Spirit,
        Demon,
        Terra,
        Knight,
        Cleric,
        Angel,
        Victory,
        Plenty
    }

    // Generic method to assign enum based on string input.
    public T AssignEnum<T>(string input) where T : struct, Enum
    {
        if (Enum.TryParse(input, true, out T result))
        {
            return result;
        }
        else
        {
            Debug.LogError($"No matching enum value for string: {input}");
            throw new ArgumentException("Invalid string for enum conversion.");
        }
    }
}
