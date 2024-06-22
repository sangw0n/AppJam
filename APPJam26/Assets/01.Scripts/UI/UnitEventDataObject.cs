using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventDataObject : MonoBehaviour
{
    [SerializeField]
    private List<BattleEventData> _unitEventDatas = new List<BattleEventData>();

    public List<BattleEventData> UnitEventDatas => _unitEventDatas;
}
