using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameProcess _gameProcess;

    private PlayerMovement _playerMovement;

    public bool IsAlive { get; private set; }

    public Action PlayerDied;
    public Action PlayerPassedObstacle;

    private void Start()
    {
        // Init variables
        _playerMovement = GetComponent<PlayerMovement>();
        IsAlive = true;

        // Init events
        _playerInput.SpacePressed += _playerMovement.Jump;
        PlayerDied += Die;
        _gameProcess.GameRestarted += ResetPlayer;
    }

    private void Update()
    {
        _playerMovement.Move();
    }

    private void ResetPlayer()
    {
        IsAlive = true;
        _playerMovement.ResetPosition();

        //_playerInput.SpacePressed += _playerMovement.Jump;

        _playerMovement.SetSimulation(true);
    }

    private void Die()
    {
        IsAlive = false;

        //_playerInput.SpacePressed -= _playerMovement.Jump;

        _playerMovement.SetSimulation(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            Debug.Log("Collide!");
            PlayerDied.Invoke();
        }
    }
}
