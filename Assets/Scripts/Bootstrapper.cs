using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    public PlayerInput PlayerInput;

    public ObjectPool PlayerBulletPool;
    public ObjectPool EnemyBulletPool;
    public ObjectPool InvaderAPool;
    public ObjectPool InvaderBPool;
    public ObjectPool InvaderCPool;
    public ObjectPool UFOPool;

    // Set up static references for other scripts
    public void Awake()
    {
        CoreController.PlayerInput = PlayerInput;
        CoreController.PlayerBulletPool = PlayerBulletPool;
        CoreController.EnemyBulletPool = EnemyBulletPool;
    }
}

public static class CoreController
{
    public static PlayerInput PlayerInput;

    public static ObjectPool PlayerBulletPool;
    public static ObjectPool EnemyBulletPool;
}
