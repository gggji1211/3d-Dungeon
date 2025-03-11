using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsumableEffect
{
    public ConsumableType type;
    public float value;    // ���� ���� (��: 1.5��)
    public float duration; // ���� �ð� (��)
}

public class Item : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public ConsumableEffect[] consumables;
}

