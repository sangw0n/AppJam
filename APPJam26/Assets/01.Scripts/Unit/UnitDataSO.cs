using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitData
{

    public string UnitName;
    public Sprite UnitSprite;
    public int MaxHealth;
    public int Strength;

}

[CreateAssetMenu(fileName = "UnitDataSO", menuName = "SO/UnitDataSO")]
public class UnitDataSO : ScriptableObject
{

    public UnitData MyUnitData;

}
