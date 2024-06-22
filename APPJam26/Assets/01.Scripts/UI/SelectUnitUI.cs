using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectUnitUI : MonoBehaviour
{
    [SerializeField]
    Unit _playerUnit;

    [SerializeField]
    List<UnitCardUI> _cards;

    private void Awake()
    {
        
        for(int i =0; i < _cards.Count; i++)
        {

            _cards[i].ClickEvent += OnClickEvent;

        }

    }

    public void OnEnable()
    {

        List<UnitDataSO> unitDatas = GameManager.Instance.UnitDatas.ToList<UnitDataSO>();

        for(int i = 0; i < _cards.Count; i++)
        {

            int randomIndex = Random.Range(i, unitDatas.Count);

            _cards[i].SetData(unitDatas[randomIndex].MyUnitData);
            unitDatas[randomIndex] = unitDatas[i];

        }


    }

    public void OnClickEvent(UnitData data)
    {
        _playerUnit.SetUnitData(data);
        gameObject.SetActive(false);
        GameManager.Instance.ChangeGameState(GameState.Setting);

    }

    


}
