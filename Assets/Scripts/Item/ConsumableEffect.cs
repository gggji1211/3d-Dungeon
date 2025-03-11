using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsumableEffect
{
    public ConsumableType type;
    public float value;    // 증가 배율 (예: 1.5배)
    public float duration; // 지속 시간 (초)
}

public class Item : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public ConsumableEffect[] consumables;
}

