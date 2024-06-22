using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float       moveSpeed;
    private Vector3     direction;
    private bool        isHit = false;
    private Unit        myUnit;

    private SpriteRenderer       spriteRenderer;
    private UnitRangeAttackEvent unitRangeAttackEvent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckDistanceFromEnemy();
        Move();
    }

    public void Initialized(UnitRangeAttackEvent unitRangeAttackEvent, Unit myUnit, Vector3 direction)
    {
        this.unitRangeAttackEvent   = unitRangeAttackEvent;
        this.myUnit                 = myUnit;
        this.direction              = direction;
    }

    private void Move()
    {
        transform.position += moveSpeed * direction * Time.deltaTime;
    }

    private Color color = new Color(0, 0, 0, 0);
    private void CheckDistanceFromEnemy()
    {
        if (isHit) return;

        if (Vector3.Distance(myUnit.OpponentUnit.transform.position, transform.position) < 1.0f + (moveSpeed * 0.2f))
        {
            isHit = true;

            myUnit.OpponentUnit.HitDamage(myUnit.UnitData.Strength);

            spriteRenderer.color = color;

            StartCoroutine(Co_IsHit());
        }
    }

    private WaitForSeconds waitForSeconds00 = new WaitForSeconds(0.4f);
    private IEnumerator Co_IsHit()
    {
        yield return waitForSeconds00;

        unitRangeAttackEvent.SetIsEnd(true);
        Destroy(gameObject);
    }
}
