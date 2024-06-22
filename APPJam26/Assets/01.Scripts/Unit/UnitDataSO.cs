using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitData
{

    public string UnitName;
    public string UnitDescription;
    public Sprite UnitSprite;
    public int MaxHealth;
    public int Strength;
    public UnitEventDataObject EventData;

}

[CreateAssetMenu(fileName = "UnitDataSO", menuName = "SO/UnitDataSO")]
public class UnitDataSO : ScriptableObject
{

    public UnitData MyUnitData;

}
