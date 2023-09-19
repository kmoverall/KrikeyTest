using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// Contains logic about enemy group behavior
public class WaveController : MonoBehaviour
{
    public List<List<Enemy>> Enemies;

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

    private bool _MovingRight = true;

    // Gets the position of the furthest enemies still alive
    private Bounds EnemyBounds {
        get {
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;
            Bounds result = new Bounds();
            IEnumerable<Enemy> check;

            for (int i = 0; i < Enemies[0].Count; i++)
            {
                check = Enemies.Select(r => r[i]);
                if (check.Any(e => e.gameObject.activeSelf))
                {
                    max.x = check.First().transform.localPosition.x;
                    break;
                }
            }
            for (int i = Enemies[0].Count-1; i >=0; i--)
            {
                check = Enemies.Select(r => r[i]);
                if (check.Any(e => e.gameObject.activeSelf))
                {
                    min.x = check.First().transform.localPosition.x;
                    break;
                }
            }

            var top = Enemies.First(e => e.Any(e => e.gameObject.activeSelf));
            max.y = top.First().transform.localPosition.y;

            var bottom = Enemies.Last(e => e.Any(e => e.gameObject.activeSelf));
            min.y = bottom.First().transform.localPosition.y;

            result.min = min;
            result.max = max;
            return result;
        }
    }

    public void Initialize()
    {
        foreach (var row in Enemies)
        {
            foreach (var enemy in row)
            {
                enemy.OnDestroy = OnDestroy;
            }
        }

        Enemies.Last().ForEach(e => e.CanAttack = true);
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
            Debug.Log("Hit Max");
            position.x = _XLimit - bounds.max.x;
            position.y -= _StepdownDistance;
            _MovingRight = !_MovingRight;
        }

        if (position.x + bounds.min.x < -1 * _XLimit)
        {
            Debug.Log("Hit Min");
            position.x = -1 * _XLimit - bounds.min.x;
            position.y -= _StepdownDistance;
            _MovingRight = !_MovingRight;
        }

        transform.position = position;
    }

    private void OnDestroy(Enemy target)
    {
        if (target.Column > 0)
        {
            Enemies[target.Row-1][target.Column].CanAttack = true;
        }
        _SpeedMultiplier += _SpeedPerKill;
        Enemies.ForEach(r => r.ForEach(e => e.SpeedMultiplier = _SpeedMultiplier));
    }
}
