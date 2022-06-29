using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Default parameters")]
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private float _obstacleSpacing;
    [SerializeField] private float _obstacleGapSize;

    [SerializeField] private float _obstacleStartSpawnOffset;
    [SerializeField] private int _initialNumberOfObstacles;
    [SerializeField] private int _maxNumberOfObstacles;
    

    [Header("References")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private GameProcess _gameProcess;

    //private GameObject _lastSpawnedObstacle;
    public Queue<Obstacle> _spawnedObstaclesQueue;

    private void Start()
    {
        _spawnedObstaclesQueue = new Queue<Obstacle>();
        Obstacle.SetHeightLimit(_playerCamera.GetCameraOrthographicBounds().max.y);

        SpawnInitialObstacles();
        Debug.Log(_spawnedObstaclesQueue.Count);
    }

    private void Update()
    {
        if (_gameProcess.CurrentGameState == GameState.Running)
        {
            Generate();
        }
    }

    private void Generate()
    {
        float cameraRightBorderPosition = _playerCamera.GetCameraOrthographicBounds().max.x;

        // If camera has exceeded the point of the (last spawned obstacle - Offset) with it's right border 
        if (cameraRightBorderPosition > _spawnedObstaclesQueue.Last().transform.position.x)
        {
            // If the max number of obstacles isn't exceeded
            if (_spawnedObstaclesQueue.Count < _maxNumberOfObstacles)
            {
                SpawnNewObstacle();
            }
            else // grab the first obstacle and throw it to the end of the queue
            {
                Obstacle obstacle = _spawnedObstaclesQueue.Dequeue();
                
                obstacle.transform.position = CalculateNewObstaclePosition();
                SetObstacleRandomHeight(ref obstacle);

                _spawnedObstaclesQueue.Enqueue(obstacle);
            }
        }
    }

    // Rough stuff, for debug purposes only
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.right * _playerCamera.GetCameraOrthographicBounds().max.x, 3);
    }

    //private Vector3 GetDistanceToLastObstacle()
    //{
    //    return 
    //}

    //[ExecuteAlways]
    private void SpawnInitialObstacles()
    {
        Debug.Log("SpawnInitialObstacles()");

        for (int i = 0; i < _initialNumberOfObstacles; i++)
        {
            SpawnNewObstacle();
        }
    }

    private Obstacle SpawnNewObstacle()
    {
        Vector3 newObstaclePosition = CalculateNewObstaclePosition();

        Obstacle newObstacle = Instantiate(_obstacle, newObstaclePosition, Quaternion.identity);

        newObstacle.transform.SetParent(gameObject.transform, true);
        _spawnedObstaclesQueue.Enqueue(newObstacle);

        newObstacle.GapSize = _obstacleGapSize;
        SetObstacleRandomHeight(ref newObstacle);

        Debug.Log("SpawnNewObstacle()\n\n", newObstacle);
        return newObstacle;
    }

    private void SetObstacleRandomHeight(ref Obstacle obstacle)
    {
        obstacle.GapHeight = Random.Range(-1.0f, 1.0f);
    }

    private Vector3 CalculateNewObstaclePosition()
    {
        if (_spawnedObstaclesQueue.Count == 0)
        {
            return new Vector3(_playerCamera.GetCameraOrthographicBounds().max.x + _obstacleStartSpawnOffset, 0, 0);
        }
        else
        {
            return new Vector3(_spawnedObstaclesQueue.Last().transform.position.x + _obstacleSpacing, 0, 0);
        }
    }
}
