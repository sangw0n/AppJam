using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitMeleeAttackEvent : UnitEvent
{
    [SerializeField]
    private Vector3 offsetDistanceFromEnemy = new Vector3(1.0f, 0.0f, 0.0f);
    private bool    isEnd                   = false;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(Co_Attack());
    }

    private WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);

    public IEnumerator Co_Attack()
    {
        Vector3 _tempPosition               = transform.position;
        Vector3 _offsetDistanceFromEnemy    = offsetDistanceFromEnemy;

        // ���� �� ��ġ���� ���ʿ� ���� 
        if (transform.position.x < _myUnit.OpponentUnit.transform.position.x)
            _offsetDistanceFromEnemy *= -1;

        transform.DOMove(_myUnit.OpponentUnit.transform.position + _offsetDistanceFromEnemy, 1.5f);

        // �ִϸ��̼� ����
        // ������ ������ �ֱ�

        yield return waitForSeconds;

        transform.DOMove(_tempPosition, 1.5f);
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
