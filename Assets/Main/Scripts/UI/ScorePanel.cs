using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _bestScore;

    private ScoreCounter _scoreCounter;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        // Init variables
        _scoreCounter = ProjectContext.Instance.ScoreCounter;

        // Init events
        _scoreCounter.ScoresUpdated += OnScoresUpdated;
    }

    private void UpdateScorePanel()
    {
        _currentScore.text = _scoreCounter.CurrentScore.ToString() ?? "0";
        _bestScore.text = _scoreCounter.BestScore.ToString() ?? "0";

        //Debug.Log($"SCORES UPDATED FOR {gameObject.transform.parent}" +
        //        $"\nCurrent: {_currentScore.text} " +
        //        $"| Best: {_bestScore.text}");
    }

    private void OnEnable()
    {
        UpdateScorePanel();
    }

    private void OnScoresUpdated()
    {
        UpdateScorePanel();
    }
}
