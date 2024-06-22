using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 direction;

    private void Update()
    {
        Move();

        if(transform.position.x > 8)
        {
            Destroy(gameObject);
        }
    }

    public void Initialized(Vector3 direction)
    {
        this.direction = direction;
    }

    private void Move()
    {
        transform.position += moveSpeed * direction * Time.deltaTime;
    }
}
