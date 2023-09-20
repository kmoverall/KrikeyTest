using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    [SerializeField]
    private float _Speed;

    [SerializeField]
    private float _XLimit;

    protected override void Update()
    {
        var position = transform.position;
        position.x += _Speed * Time.deltaTime;
        transform.position = position;

        if (position.x < -1 * _XLimit || position.x > _XLimit)
        {
            Destroy(gameObject);
        }
    }
}
