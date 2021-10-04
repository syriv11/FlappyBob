using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameProcess _gameProcess;

    private Transform _playerTransform;
    private Vector3 _cameraOffset;

    private void Start()
    {
        // Init variables
        _playerTransform = _playerMovement.transform;
        _cameraOffset = transform.position - _playerTransform.position;

        _gameProcess.GameRestarted += StartRewind;
    }

    private void Update()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        Debug.Log("Track!");
        transform.position = new Vector3(_cameraOffset.x + _playerTransform.position.x, _cameraOffset.y, _cameraOffset.z);
    }

    private void StartRewind()
    {
        Debug.Log("Rewind!");
        StartCoroutine("Rewind");
    }

    private IEnumerable Rewind()
    {
        for (float lerp = 1.0f; lerp >= 0; lerp -= 0.001f)
        {
            transform.position = Vector3.Lerp(transform.position, _playerMovement.StartPosition, lerp);
            yield return null;
        }
    }
}
