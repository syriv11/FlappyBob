using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBird : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Parameters")]
    [SerializeField] private float _jumpHeight;

    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody;
    private bool _isAlive;

    public Action PlayerDead;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _playerInput.SpacePressed += Jump;
        _isAlive = true;
        PlayerDead += Dead;
    }

    private void Update()
    {
        Move();
        //Test
        gameObject.transform.rotation = Quaternion.Euler(_rigidbody.velocity);
        //Debug.Log($"Current rotation: {gameObject.transform.rotation}");
        //Debug.Log($"Velocity: {_rigidbody.velocity}");
    }

    private void Jump()
    {
        if(_isAlive)
        {
            Debug.Log("Jump");
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Force);
        }
    }

    private void Move()
    {
        if(_isAlive)
        {
            gameObject.transform.position += Vector3.right * _movementSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        PlayerDead.Invoke();
        //_playerInput.SpacePressed -= Jump;
    }

    private void Dead()
    {
        _isAlive = false;
    }
}
