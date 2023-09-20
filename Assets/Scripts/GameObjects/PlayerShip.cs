using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerShip : MonoBehaviour
{
    private InputAction _MoveAction;
    private InputAction _FireAction;

    [SerializeField]
    private float MovementLimit;
    [SerializeField]
    private float MovementSpeed;

    public int LifeCount;

    public Action OnDestroy;


    private void Start()
    {
        _MoveAction = CoreController.PlayerInput.actions["Move"];
        _FireAction = CoreController.PlayerInput.actions["Fire"];
        _FireAction.started += Fire;
    }

    private void Update()
    {
        var moveValue = _MoveAction.ReadValue<Vector2>().x;

        var position = transform.position;
        position.x += -1 * MovementSpeed * moveValue * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -1 * MovementLimit, MovementLimit);

        transform.position = position;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        CoreController.PlayerBulletPool.Create(transform.position);
    }

    public void Reset()
    {
        var position = transform.position;
        position.x = 0;
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeSelf && other.gameObject.activeSelf)
        {
            other.GetComponent<Bullet>()?.Hit();
            LifeCount -= 1;
            OnDestroy?.Invoke();
        }
    }
}
