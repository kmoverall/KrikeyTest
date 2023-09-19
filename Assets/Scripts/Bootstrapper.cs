using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public ObjectPool BulletPool;
    public ObjectPool InvaderAPool;
    public ObjectPool InvaderBPool;
    public ObjectPool InvaderCPool;
    public ObjectPool UFOPool;

    public void Awake()
    {
        CoreController.BulletPool = BulletPool;
        CoreController.InvaderAPool = InvaderAPool;
        CoreController.InvaderBPool = InvaderBPool;
        CoreController.InvaderCPool = InvaderCPool;
        CoreController.UFOPool = UFOPool;
    }
}

public static class CoreController
{
    public static ObjectPool BulletPool;
    public static ObjectPool InvaderAPool;
    public static ObjectPool InvaderBPool;
    public static ObjectPool InvaderCPool;
    public static ObjectPool UFOPool;
}
