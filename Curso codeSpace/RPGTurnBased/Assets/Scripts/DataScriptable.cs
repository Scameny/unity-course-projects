using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Database", menuName = "DataScriptable/DataItem")]
public class DataScriptable : ScriptableObject
{
    public List<Item> items;
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string description;
    public int effectAmount;
    public int amount;
}
