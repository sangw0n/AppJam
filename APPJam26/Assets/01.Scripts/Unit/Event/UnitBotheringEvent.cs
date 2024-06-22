using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBotheringEvent : UnitEvent
{
    private bool _isEnd = false;

    public override void Init()
    {
        _isEnd = false;
    }

    public override void InvokeEvent()
    {
        int index = Random.Range(0, _commentary.Length);
        StartCoroutine(GameManager.Instance.Co_InputText(_commentary[index]));

        StartCoroutine(DelayCo());
    }

    WaitForSeconds wait1 = new WaitForSeconds(1);
    IEnumerator DelayCo()
    {
        yield return wait1;
        _isEnd = true;
    }

    public override bool IsEnd()
    {
        return _isEnd;
        
    }
}
