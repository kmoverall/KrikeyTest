using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public bool CanAttack;

    private float _SpeedMultiplier = 1;

    [SerializeField]
    private float _FiringInterval;

    //Prevents enemies from attacking for the first x seconds of gameplay
    [SerializeField]
    private float _GracePeriod;

    private float _AttackTimer;
    private float _GraceTimer;

    private Animator _Animator;

    public Action<Enemy> OnDestroy;

    [SerializeField]
    private float _PointValue;

    [System.NonSerialized]
    public int Row;
    [System.NonSerialized]
    public int Column;

    public float SpeedMultiplier 
    {
        get => _SpeedMultiplier;
        set {
            _SpeedMultiplier = value;
            _Animator.speed = _SpeedMultiplier;
        }
    }

    public void Initialize(int r, int c)
    {
        Row = r;
        Column = c;

        // Randomly stagger attack interval
        _AttackTimer = UnityEngine.Random.Range(0, _FiringInterval);
        _GraceTimer = _GracePeriod;
        _Animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (_GraceTimer > 0)
        {
            _GraceTimer -= Time.deltaTime;
        }

        _AttackTimer -= Time.deltaTime * SpeedMultiplier;

        if (CanAttack && _AttackTimer <= 0 && _GraceTimer <= 0)
        {
            _AttackTimer = _FiringInterval * UnityEngine.Random.Range(0.9f, 1.1f);
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

            OnDestroy?.Invoke(this);
        }
    }
}
