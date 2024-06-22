using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpikeBulletEvent : UnitEvent
{
    [SerializeField]
    private GameObject _spikeBulletPrefab;
    private bool _isEnd = false;

    public override void Init()
    {
        _isEnd = false;
    }

    public override void InvokeEvent()
    {
        StartCoroutine(BulletCo());
    }

    WaitForSeconds wait021 = new WaitForSeconds(0.31f);
    IEnumerator BulletCo()
    {

        Vector3 myUnitPos = _myUnit.transform.position;
        float dist = Vector3.Distance(myUnitPos, _myUnit.OpponentUnit.transform.position);

        for(int i = 0; i < 5; ++i)
        {
            // -90도 기준으로 90도까지 5개의 탄환 { -90, -45, 0, 45, 90 }
            float angle = (-90f + (i * 45f)) * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            GameObject spikeBullet = Instantiate(_spikeBulletPrefab, myUnitPos, Quaternion.identity);
            spikeBullet.transform.right = dir;

            spikeBullet.transform.DOMove(myUnitPos + dir * dist, 0.3f).SetEase(Ease.OutCubic);

        }

        yield return wait021;
        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);

        yield return wait021;
        _isEnd = true;

    }

    public override bool IsEnd()
    {
        return _isEnd;
    }
}
