using System;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private Camera _playerCamera;
    private Collider2D _playerCollider;

    void Start()
    {
        Init();
    }

    public void TryTeleportPlayer()
    {
        if (IsPlayerExceededUpperBorder())
        {
            TeleportPlayerToBottomBorder();
            return;
        }

        if (IsPlayerExceededBottomBorder())
        {
            TeleportPlayerToUpperBorder();
        }
    }

    private void Init()
    {
        // Init variables
        _playerCamera = ProjectContext.Instance.PlayerCamera;
        _playerCollider = GetComponent<Collider2D>();
    }

    private bool IsPlayerExceededUpperBorder() => _playerCollider.bounds.min.y > GetCameraBounds().max.y;
    private bool IsPlayerExceededBottomBorder() => _playerCollider.bounds.max.y < GetCameraBounds().min.y;

    private void TeleportPlayerToUpperBorder()
    {
        transform.position += Vector3.up * (GetCameraBounds().size.y + _playerCollider.bounds.size.y);
    }

    private void TeleportPlayerToBottomBorder()
    {
        transform.position += Vector3.down * (GetCameraBounds().size.y + _playerCollider.bounds.size.y);
    }

    private Bounds GetCameraBounds() => _playerCamera.GetCameraOrthographicBounds();
}
