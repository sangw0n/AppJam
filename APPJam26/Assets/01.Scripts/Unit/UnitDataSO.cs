
using UnityEngine;

[System.Serializable]
public struct UnitData
{

    public string UnitName;
    public string UnitDescription;
    public Sprite UnitSprite;
    public Sprite UnitUISprite;
    public Sprite UnitMouseOnUISprite;
    public int MaxHealth;
    public int Strength;
    public UnitEventDataObject EventData;
    public RuntimeAnimatorController UnitAnimController;

}

[CreateAssetMenu(fileName = "UnitDataSO", menuName = "SO/UnitDataSO")]
public class UnitDataSO : ScriptableObject
{

    public UnitData MyUnitData;

}
