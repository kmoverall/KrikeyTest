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
    }

    private void OnDestroy(Enemy target)
    {
        if (target.Column > 0)
        {
            Enemies[target.Row][target.Column].CanAttack = true;
        }
        _SpeedMultiplier += _SpeedPerKill;
        Enemies.ForEach(r => r.ForEach(e => e.SpeedMultiplier = _SpeedMultiplier));
    }
}
