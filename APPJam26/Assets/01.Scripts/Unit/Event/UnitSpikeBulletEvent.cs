using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpikeBulletEvent : UnitEvent
{
    [SerializeField]
    private GameObject _spikeBulletPrefab;
    List<GameObject> _spikeObjList = new List<GameObject>();
    private bool _isEnd = false;

    [SerializeField]
    private int _count = 1;

    public override void Init()
    {
        _isEnd = false;
    }

    public override void InvokeEvent()
    {
        int index = Random.Range(0, _commentary.Length);
        StartCoroutine(GameManager.Instance.Co_InputText(_commentary[index]));

        StartCoroutine(BulletCo());
    }

    WaitForSeconds wait021 = new WaitForSeconds(0.41f);
    IEnumerator BulletCo()
    {

        Vector3 myUnitPos = _myUnit.transform.position;
        float dist = Vector3.Distance(myUnitPos, _myUnit.OpponentUnit.transform.position);

        for(int j = 0; j < _count; j++)
        {

            for (int i = 0; i < 5; ++i)
            {
                // -180도 기준으로 0도까지 5개의 탄환 { -90, -45, 0, 45, 90 }
                float angle = (180f - (i * 45f)) * Mathf.Deg2Rad;
                Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

                GameObject spikeBullet = Instantiate(_spikeBulletPrefab, myUnitPos, Quaternion.identity);
                spikeBullet.transform.right = dir;

                spikeBullet.transform.DOMove(myUnitPos + dir * dist, 0.4f).SetEase(Ease.OutCubic);
                _spikeObjList.Add(spikeBullet);
            }

            yield return wait021;
            _spikeObjList.ForEach(e => Destroy(e));
            _spikeObjList.Clear();
            _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);

        }

        yield return wait021;
        _isEnd = true;

    }

    public override bool IsEnd()
    {
        return _isEnd;
    }
}
