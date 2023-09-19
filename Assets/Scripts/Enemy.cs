using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool CanAttack;
    [System.NonSerialized]
    public float SpeedMultiplier = 1;

    [SerializeField]
    private float _FiringInterval;

    //Prevents enemies from attacking for the first x seconds of gameplay
    [SerializeField]
    private float _GracePeriod;

    private float _AttackTimer;
    private float _GraceTimer;

    private void Start()
    {
        Initalize();
    }

    public void Initalize()
    {
        _AttackTimer = Random.Range(0, _FiringInterval);
        _GraceTimer = _GracePeriod;
    }

    private void Update()
    {
        if (_GraceTimer > 0)
        {
            _GraceTimer -= Time.deltaTime;
        }

        _AttackTimer -= Time.deltaTime * SpeedMultiplier;

        if (CanAttack && _AttackTimer <= 0 && _GraceTimer <= 0)
        {
            _AttackTimer = _FiringInterval;
            Fire();
        }
    }

    private void Fire()
    {
        CoreController.EnemyBulletPool.Create(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeSelf && other.gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            other.GetComponent<Bullet>()?.Hit();
        }
    }
}
