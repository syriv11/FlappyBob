using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    public int BestScore { get; private set; }
    public Action ScoresUpdated;

    private Player _player;
    private GameProcess _gameProcess;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // Init variables
        CurrentScore = 0;
        BestScore = 0;

        _player = ProjectContext.Instance.Player;
        _gameProcess = ProjectContext.Instance.GameProcess;

        // Init events
        _player.PlayerPassedObstacle += OnPlayerPassedObstacle;
        _gameProcess.GameStateSwitched += OnGameStateSwitched;
    }

    public void AddScore()
    {
        CurrentScore++;

        if (IsBestScoreExeeded())
            BestScore++;

        ScoresUpdated.Invoke();
    }

    public void ResetCurrentScores()
    {
        CurrentScore = 0;
        ScoresUpdated.Invoke();
    }

    private void SaveBestScoreOnComputer()
    {
        // Have no idea how and does it actually matters? Just threw a random idea that came to my mind
    }

    private bool IsBestScoreExeeded() => CurrentScore > BestScore;

    private void OnPlayerPassedObstacle(Obstacle obstacle)
    {
        AddScore();
    }

    private void OnGameStateSwitched(GameState newGameState)
    {
        if (newGameState is MainMenuState)
            ResetCurrentScores();
    }
}
