using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    private float       moveSpeed;
    private Vector2     direction;
    private bool        isHit = false;

    private Rigidbody2D          rigid;
    private UnitRangeAttackEvent unitRangeAttackEvent;

    public bool         IsHit => isHit;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Initialized(UnitRangeAttackEvent unitRangeAttackEvent, Vector2 direction)
    {
        this.unitRangeAttackEvent = unitRangeAttackEvent;
        this.direction = direction;
    }

    private void Move()
    {
        rigid.MovePosition(rigid.position + (moveSpeed * direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이 맞으면 파티클 발생

        // 적 피격 
        unitRangeAttackEvent.SetIsEnd(true);
    }
}
