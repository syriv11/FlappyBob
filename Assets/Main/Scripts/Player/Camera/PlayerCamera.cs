using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
[RequireComponent(typeof(CameraAnimation))]
public class PlayerCamera : MonoBehaviour
{
    private CameraMovement _cameraMovement;
    private CameraAnimation _cameraAnimation;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsAlive && !_cameraAnimation.IsAnimationRunning)
        {
            _cameraMovement.TrackPlayer();
        }
    }

    private void Init()
    {
        // Init variables
        _cameraMovement = GetComponent<CameraMovement>();
        _cameraAnimation = GetComponent<CameraAnimation>();
        _player = ProjectContext.Instance.Player;

        _cameraAnimation.SetCameraMovement(_cameraMovement);
    }
}
