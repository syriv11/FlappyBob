using UnityEngine;

[ExecuteAlways, SelectionBase]
public class Obstacle : MonoBehaviour
{
    public static float _heightLimit;

    [Header("Parameters")]
    [Range(-1, 1)] public float GapHeight;
    [Min(0)]       public float GapSize;

    [SerializeField, Min(0)] private float _gapHeightMargin;

    [Header("References")]
    [SerializeField] private GameObject _upperWall;
    [SerializeField] private GameObject _bottomWall;

    private float _initialWallsOffset;

    private void Start()
    {
        _initialWallsOffset = _upperWall.transform.localPosition.y;

        CalculateWallsPosition();
    }

    private void Update()
    {
        // Editor logic
        if (!Application.IsPlaying(gameObject))
        {
            CalculateWallsPosition();
        }
    }

    // Not sure if this method is written correctly
    private void SetObstacleRandomHeight(ref Obstacle obstacle)
    {
        obstacle.GapHeight = Random.Range(-1.0f, 1.0f);

        // I stopped right here. It almost done in very rough way, only some +- minor fixes and polishing needed.
    }

    public void CalculateWallsPosition()
    {
        float wallsGapSizeOffset = _initialWallsOffset + (GapSize / 2);
        float wallsGapHeightOffset = _heightLimit * GapHeight;

        _upperWall.transform.localPosition = Vector3.up * (wallsGapSizeOffset - wallsGapHeightOffset);
        _bottomWall.transform.localPosition = Vector3.down * (wallsGapSizeOffset + wallsGapHeightOffset);
    }

    public static void SetHeightLimit(float heightLimit) => _heightLimit = heightLimit;
}
