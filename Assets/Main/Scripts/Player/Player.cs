using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerTeleporter))]
public class Player : MonoBehaviour, IPausable
{
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerTeleporter PlayerTeleporter { get; private set; }
    public bool IsAlive { get; private set; }

    public Action PlayerDied;
    public Action<Obstacle> PlayerPassedObstacle;

    private PlayerInputManager _playerInput;
    private GameProcess _gameProcess;
    private PauseManager _pauseManager;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        PlayerMovement.Move();
        PlayerTeleporter.TryTeleportPlayer();
    }

    private void Respawn()
    {
        IsAlive = true;
        PlayerMovement.ResetTransform();
    }

    private void Die()
    {
        Debug.Log("Die");

        IsAlive = false;
        PlayerMovement.SetSimulation(false);
        PlayerDied?.Invoke();
    }

    private void HandlePlayerPassedObtacle(Obstacle obstacle)
    {
        Debug.Log("PassedObstacle");

        obstacle.MarkAsPassed();
        PlayerPassedObstacle?.Invoke(obstacle);
    }

    private void Init()
    {
        // Init variables
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerTeleporter = GetComponent<PlayerTeleporter>();

        _playerInput = ProjectContext.Instance.PlayerInputManager;
        _pauseManager = ProjectContext.Instance.PauseManager;
        _gameProcess = ProjectContext.Instance.GameProcess;

        IsAlive = true;

        // Init events
        _playerInput.JumpPressed += PlayerMovement.Jump;
        _gameProcess.GameStateSwitched += OnGameStateSwitched;

        _pauseManager.Register(this);
    }

    void IPausable.SetPause(bool isPaused)
    {
        PlayerMovement.SetSimulation(!isPaused);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent.TryGetComponent<Obstacle>(out Obstacle obstacle))
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            if (!obstacle.IsPassed)
                HandlePlayerPassedObtacle(obstacle);
        }
    }

    private void OnGameStateSwitched(GameState newGameState)
    {
        if (newGameState is MainMenuState)
            Respawn();
    }
}
