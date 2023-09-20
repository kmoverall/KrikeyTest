using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


// Contains logic about enemy group behavior
public class WaveController : MonoBehaviour
{
    private List<List<Enemy>> _Enemies;

    [SerializeField]
    private float _InitialSpeed;

    [SerializeField]
    private float _SpeedMultiplier;

    [SerializeField]
    private float _SpeedPerKill;

    [SerializeField]
    private float _XLimit;

    [SerializeField]
    private float _StepdownDistance;

    [SerializeField]
    private float _YLimit;

    private bool _MovingRight = true;

    public Action OnYLimitHit; 

    // Gets the position of the furthest enemies still alive
    private Bounds EnemyBounds {
        get {
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;
            Bounds result = new Bounds();
            IEnumerable<Enemy> check;

            for (int i = 0; i < _Enemies[0].Count; i++)
            {
                check = _Enemies.Select(r => r[i]);
                if (check.Any(e => e.gameObject.activeSelf))
                {
                    max.x = check.First().transform.localPosition.x;
                    break;
                }
            }
            for (int i = _Enemies[0].Count-1; i >=0; i--)
            {
                check = _Enemies.Select(r => r[i]);
                if (check.Any(e => e.gameObject.activeSelf))
                {
                    min.x = check.First().transform.localPosition.x;
                    break;
                }
            }

            var top = _Enemies.FirstOrDefault(e => e.Any(e => e.gameObject.activeSelf));
            max.y = top?.FirstOrDefault()?.transform.localPosition.y ?? 0;

            var bottom = _Enemies.LastOrDefault(e => e.Any(e => e.gameObject.activeSelf));
            min.y = bottom?.FirstOrDefault()?.transform.localPosition.y ?? 0;

            result.min = min;
            result.max = max;
            return result;
        }
    }

    public void Reset()
    {
        _SpeedMultiplier = 1;

        _Enemies = null;
        enabled = false;

        var position = transform.position;
        position.y = 0;
        transform.position = position;
    }

    public void Initialize(List<List<Enemy>> enemies)
    {
        _Enemies = enemies;
        foreach (var row in _Enemies)
        {
            foreach (var enemy in row)
            {
                enemy.OnDestroyed = OnDestroy;
            }
        }

        _Enemies.Last().ForEach(e => e.CanAttack = true);
        enabled = true;
    }

    private void Update()
    {
        var sign = _MovingRight ? -1 : 1;

        var position = transform.position;
        // EnemyBounds is expensive, so we're making sure its only called once per Update
        var bounds = EnemyBounds;

        position.x += sign * _InitialSpeed * _SpeedMultiplier * Time.deltaTime;

        if (position.x + bounds.max.x > _XLimit)
        {
            position.x = _XLimit - bounds.max.x;
            position.y -= _StepdownDistance;
            _MovingRight = !_MovingRight;
        }

        if (position.x + bounds.min.x < -1 * _XLimit)
        {
            position.x = -1 * _XLimit - bounds.min.x;
            position.y -= _StepdownDistance;
            _MovingRight = !_MovingRight;
        }

        transform.position = position;

        if (position.y + bounds.min.y <= _YLimit)
        {
            OnYLimitHit?.Invoke();
        }
    }

    public void Stop()
    {
        enabled = false;
        _Enemies = null;
    }

    private void OnDestroy(Enemy target)
    {
        if (target.Row > 0)
        {
            _Enemies[target.Row-1][target.Column].CanAttack = true;
        }
        _SpeedMultiplier += _SpeedPerKill;
        _Enemies.ForEach(r => r.ForEach(e => e.SpeedMultiplier = _SpeedMultiplier));
        CoreController.EnemyManager.OnEnemyDestroyed();
    }
}
