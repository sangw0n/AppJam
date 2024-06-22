using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitMeleeAttackEvent : UnitEvent
{
    [SerializeField]
    private Vector3 offsetDistanceFromEnemy = new Vector3(1.0f, 0.0f, 0.0f);
    [SerializeField]
    private float   moveSpeed;
    private bool    isEnd                   = false;
    private Transform _unitTransform;

    private WaitForSeconds waitForSecondsSpeed;

    public override void Init()
    {
        isEnd = false;

        _unitTransform = _myUnit.transform;

        waitForSecondsSpeed = new WaitForSeconds(moveSpeed + 0.1f);
    }

    public override void InvokeEvent()
    {
        StartCoroutine(Co_Attack());
    }

    private WaitForSeconds waitForSeconds00 = new WaitForSeconds(1.6f);
    private WaitForSeconds waitForSeconds01 = new WaitForSeconds(0.3f);
    private WaitForSeconds waitForSeconds02 = new WaitForSeconds(0.1f);

    public IEnumerator Co_Attack()
    {
        Vector3 _tempPosition               = _unitTransform.position;
        Vector3 _offsetDistanceFromEnemy    = offsetDistanceFromEnemy;

        // 적 유닛이 오른쪽에 있으면 
        if (_unitTransform.position.x < _myUnit.OpponentUnit.transform.position.x)
            _offsetDistanceFromEnemy.x  *= -1;

        _unitTransform.DOMove(_myUnit.OpponentUnit.transform.position + _offsetDistanceFromEnemy, moveSpeed);

        // 애니메이션 
        _myUnit.Animator.SetTrigger(_myUnit.HASH_ATTACK);
        yield return waitForSecondsSpeed;
        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);
        yield return waitForSeconds02;
        // 파티클 생성 

        _unitTransform.DOMove(_tempPosition, moveSpeed);

        yield return waitForSecondsSpeed;

        // 나의 턴 종료 
        isEnd = true;   

    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
