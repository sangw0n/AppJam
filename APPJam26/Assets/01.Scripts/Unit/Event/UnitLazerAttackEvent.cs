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
        int index = Random.Range(0, _commentary.Length);
        StartCoroutine(GameManager.Instance.Co_InputText(_commentary[index]));

        StartCoroutine(Attack());
    }

    [SerializeField]
    private Vector3 offset;
    private IEnumerator Attack()
    {
        _myUnit.Animator.SetTrigger(_myUnit.HASH_ATTACK);

        // 적 유닛이 오른쪽에 있으면 
        if (_myUnit.transform.position.x < _myUnit.OpponentUnit.transform.position.x)
        {
            GameObject cloneLazer = Instantiate(lazerPrefab, _myUnit.transform.position +  new Vector3(4.5f, -0.8f, 0.0f), Quaternion.identity);
            cloneLazer.transform.rotation = Quaternion.Euler(0, 0, 0);
            cloneLazer.GetComponent<Lazer>().Initialized(Vector3.right);

        }
        else
        {
            GameObject cloneLazer = Instantiate(lazerPrefab, _myUnit.transform.position - new Vector3(4.5f, 0.8f, 0.0f), Quaternion.identity);
            cloneLazer.transform.rotation = Quaternion.Euler(0, 0, 180);
            cloneLazer.GetComponent<Lazer>().Initialized(Vector3.left);
        }


        yield return new WaitForSeconds(0.2f);

        _myUnit.OpponentUnit.HitDamage(_myUnit.UnitData.Strength);

        yield return new WaitForSeconds(0.7f);

        isEnd = true;
    }

    public override bool IsEnd()
    {
        return isEnd;
    }
}
