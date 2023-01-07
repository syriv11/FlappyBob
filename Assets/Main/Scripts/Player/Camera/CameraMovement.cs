using System;
using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Player _player;
    private Transform _playerTransform;

    private Vector3 _playerStartPosition;
    private Vector3 _cameraOffset;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // Init variables
        _player = ProjectContext.Instance.Player;
        _playerTransform = _player.transform;
        _playerStartPosition = _player.PlayerMovement.StartPosition;

        _cameraOffset = transform.position - _playerStartPosition;
    }

    public void TrackPlayer()
    {
        transform.position = GetCameraPositionNextToPlayer();
    }

    public Vector3 GetCameraPositionNextToPlayer() => new Vector3(_cameraOffset.x + _playerTransform.position.x, _cameraOffset.y, _cameraOffset.z);
}
