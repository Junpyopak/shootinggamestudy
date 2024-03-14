using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static string GetGameTag(GameTag _value)
    {
        return _value.ToString();
    }


    public static bool IsEnterFirstScene = false;//스태틱으로 선언하여 어디든 불러올수 있도록
}

public enum GameTag
{
    None,
    Enemy,
    Player,
    Item
}
