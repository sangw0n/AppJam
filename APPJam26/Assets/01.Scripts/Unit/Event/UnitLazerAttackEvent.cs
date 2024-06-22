using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLazerAttackEvent : UnitEvent
{
    [SerializeField]
    private GameObject lazerPrefab;

    private bool isEnd = false;

    public override void Init()
    {
        isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(Attack());
    }

    [SerializeField]
    private Vector3 offset;
    private IEnumerator Attack()
    {
        _myUnit.Animator.SetTrigger(_myUnit.HASH_ATTACK);
        GameObject cloneLazer = Instantiate(lazerPrefab, GameManager.Instance.CenterPos.transform.position - new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);

        yield return new WaitForSeconds(1.0f);

        isEnd = true;
        Destroy(cloneLazer);
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
