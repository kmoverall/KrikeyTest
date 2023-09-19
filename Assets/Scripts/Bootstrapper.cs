using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    public PlayerInput PlayerInput;

    public ObjectPool BulletPool;
    public ObjectPool InvaderAPool;
    public ObjectPool InvaderBPool;
    public ObjectPool InvaderCPool;
    public ObjectPool UFOPool;

    // Set up static references for other scripts
    public void Awake()
    {
        CoreController.PlayerInput = PlayerInput;
        CoreController.BulletPool = BulletPool;
        CoreController.InvaderAPool = InvaderAPool;
        CoreController.InvaderBPool = InvaderBPool;
        CoreController.InvaderCPool = InvaderCPool;
        CoreController.UFOPool = UFOPool;
    }
}

public static class CoreController
{
    public static PlayerInput PlayerInput;

    public static ObjectPool BulletPool;
    public static ObjectPool InvaderAPool;
    public static ObjectPool InvaderBPool;
    public static ObjectPool InvaderCPool;
    public static ObjectPool UFOPool;
}
