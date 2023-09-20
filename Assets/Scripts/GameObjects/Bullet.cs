using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _Speed;

    [SerializeField]
    private float _DespawnBottom;
    [SerializeField]
    private float _DespawnTop;

    // Prefer adding movement stuff to FixedUpdate
    private void FixedUpdate()
    {
        var position = transform.position;
        position.y += _Speed * Time.fixedDeltaTime;
        transform.position = position;
    }

    // Handles bullets that miss
    private void Update()
    {
        if (transform.position.y > _DespawnTop)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y < _DespawnBottom)
        {
            gameObject.SetActive(false);
        }
    }

    public void Hit()
    {
        gameObject.SetActive(false);
    }
}
