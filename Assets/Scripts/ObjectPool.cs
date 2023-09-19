using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _Prefab;
    private List<GameObject> _Pool = new List<GameObject>();
    private Transform _PoolRoot;

    public GameObject Obtain(Vector3 position)
    {
        GameObject newObject = null;
        foreach (var obj in _Pool)
        {
            if (!obj.activeSelf)
            {
                newObject = obj;
                break;
            }
        }

        if (newObject == null)
        {
            newObject = Object.Instantiate(_Prefab, _PoolRoot);
            _Pool.Add(newObject);
        }

        newObject.transform.position = position;
        newObject.SetActive(true);

        return newObject;
    }

    public void Return(GameObject toReturn)
    {
        toReturn.SetActive(false);
    }
}
