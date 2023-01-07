using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Obstacle parameters")]
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private float _obstacleSpacing;
    [SerializeField] private float _obstacleGapSize;
    [SerializeField] private float _obstacleGapMargin;

    [Header("Generation parameters")]
    [SerializeField] private float _obstacleStartSpawnOffset;
    [SerializeField] private int _initialNumberOfObstacles;
    [SerializeField] private int _maxNumberOfObstacles;
    

    private Camera _playerCamera;
    private PauseManager _pauseManager;
    private GameProcess _gameProcess;

    //private GameObject _lastSpawnedObstacle;
    private Queue<Obstacle> _spawnedObstaclesQueue;
    private float _firstObstacleStartSpawnPosition;

    private void Start()
    {
        Init();
        SpawnInitialObstacles();
    }

    private void Update()
    {
        if (_pauseManager.IsPaused)
            return;

        Generate();
    }

    private void Generate()
    {
        // If camera has exceeded the point of the (last spawned obstacle - Offset) with it's right border 
        if (GetCameraRightBorderPosition() > _spawnedObstaclesQueue.Last().transform.position.x)
        {
            // If the max number of obstacles isn't exceeded
            if (_spawnedObstaclesQueue.Count < _maxNumberOfObstacles)
            {
                SpawnNewObstacle();
            }
            else // grab the first obstacle and throw it to the end of the queue
            {
                Obstacle obstacle = _spawnedObstaclesQueue.Dequeue();

                obstacle.transform.position = CalculateObstacleNewPosition();
                obstacle.Refresh();

                _spawnedObstaclesQueue.Enqueue(obstacle);
            }
        }
    }

    private void SpawnInitialObstacles()
    {
        for (int i = 0; i < _initialNumberOfObstacles; i++)
            SpawnNewObstacle();
    }

    private Obstacle SpawnNewObstacle()
    {
        Vector3 newObstaclePosition = CalculateObstacleNewPosition();

        Obstacle newObstacle = Instantiate(_obstacle, newObstaclePosition, Quaternion.identity);

        newObstacle.transform.SetParent(gameObject.transform, true);
        _spawnedObstaclesQueue.Enqueue(newObstacle);


        newObstacle.GapSize = _obstacleGapSize;
        newObstacle.GapMargin = _obstacleGapMargin;
        newObstacle.Refresh();

        return newObstacle;
    }

    private Vector3 CalculateObstacleNewPosition()
    {
        if (_spawnedObstaclesQueue.Count == 0)
        {
            return new Vector3(_firstObstacleStartSpawnPosition, 0, 0);
        }
        else
        {
            return new Vector3(_spawnedObstaclesQueue.Last().transform.position.x + _obstacleSpacing, 0, 0);
        }
    }

    private void RestartGenerator()
    {
        DeleteOldObstacles();
        SpawnInitialObstacles();
    }

    private void DeleteOldObstacles()
    {
        foreach (Obstacle obstacle in _spawnedObstaclesQueue)
            Destroy(obstacle.gameObject);

        _spawnedObstaclesQueue.Clear();
    }

    private void Init()
    {
        // Init variables
        _spawnedObstaclesQueue = new Queue<Obstacle>();

        _playerCamera = ProjectContext.Instance.PlayerCamera;
        _pauseManager = ProjectContext.Instance.PauseManager;
        _gameProcess = ProjectContext.Instance.GameProcess;

        _firstObstacleStartSpawnPosition = GetCameraRightBorderPosition() + _obstacleStartSpawnOffset;

        // Init events
        _gameProcess.GameStateSwitched += OnGameStateSwitched;
    }

    private float GetCameraRightBorderPosition() => _playerCamera.GetCameraOrthographicBounds().max.x;

    private void OnGameStateSwitched(GameState newGameState)
    {
        if (newGameState is MainMenuState)
            RestartGenerator();
    }

    ///////////////////////////////////// Rough stuff, for debug purposes only /////////////////////////////////////
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) // I know it's bullshit...
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.right * GetCameraRightBorderPosition(), 3);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
