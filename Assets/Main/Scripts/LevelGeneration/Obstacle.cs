using UnityEngine;

[SelectionBase]
public class Obstacle : MonoBehaviour
{
    [Header("Parameters")]
    [Range(-1, 1)] public float GapHeight;
    [Min(0)]       public float GapSize;
    [Min(0)]       public float GapMargin;

    [Header("References")]
    [SerializeField] private GameObject _upperWall;
    [SerializeField] private GameObject _bottomWall;

    private Camera _playerCamera;
    private float _initialWallsOffset;

    public bool IsPassed { get; private set; }

    private void Awake()
    {
        Init();
    }

    ////////////////////////////////// Just for testing purposes so you can tweak values in playmode /////////
    ////////////////////////////////// and see the result of calculation                             /////////
    //private void Update()
    //{
    //    CalculateWallsPosition();
    //}
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Init()
    {
        // Init variables
        IsPassed = false;
        _initialWallsOffset = _upperWall.transform.localPosition.y;
        _playerCamera = ProjectContext.Instance.PlayerCamera;
    }

    private void CalculateWallsPosition()
    {
        float wallsGapSizeOffset = _initialWallsOffset + (GapSize / 2);
        float wallsGapHeightOffset = GetGapHeightLimit() * GapHeight;

        _upperWall.transform.localPosition = Vector3.up * (wallsGapSizeOffset - wallsGapHeightOffset);
        _bottomWall.transform.localPosition = Vector3.down * (wallsGapSizeOffset + wallsGapHeightOffset);
    }

    private float GetGapHeightLimit()
    {
        return GetCameraUpperBorderPosition() - GapMargin - (GapSize / 2);
    }

    private float GetCameraUpperBorderPosition() => _playerCamera.GetCameraOrthographicBounds().max.y;

    private void SetObstacleRandomHeight()
    {
        GapHeight = Random.Range(-1.0f, 1.0f);
    }

    public void MarkAsPassed()
    {
        IsPassed = true;
        // Maybe later this will also launch an animation or other stuff
    }

    public void Refresh()
    {
        IsPassed = false;
        // Maybe later also reset an animation?
        SetObstacleRandomHeight();
        CalculateWallsPosition();
    }
}
