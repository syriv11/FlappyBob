using UnityEngine;

[RequireComponent(typeof(GameProcess))]
[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PauseManager))]
[RequireComponent(typeof(ScoreCounter))]
public class ProjectContext : MonoBehaviour
{
    public static ProjectContext Instance { get; private set; }

    public GameProcess GameProcess { get; private set; }
    public PlayerInputManager PlayerInputManager { get; private set; }
    public PauseManager PauseManager { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }

    [Header("References")]
    public Player Player;
    public Camera PlayerCamera;

    private void Awake()
    {
        CreateSingletone();
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        PlayerInputManager = gameObject.GetComponent<PlayerInputManager>();
        PauseManager = gameObject.GetComponent<PauseManager>();
        GameProcess = gameObject.GetComponent<GameProcess>();
        ScoreCounter = gameObject.GetComponent<ScoreCounter>();
    }

    private void CreateSingletone()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
}
