using UnityEngine;
using System;
using System.Collections.Generic;

public class GameProcess : MonoBehaviour
{   
    public GameState CurrentGameState { get; private set; }
    public Action<GameState> GameStateSwitched;

    private PlayerInputManager _playerInputManager;
    private PauseManager _pauseManager;
    private Player _player;

    private RunningGameState _runningGameState;
    private PausedGameState _pausedGameState;
    private DeathScreenState _deathScreenState;
    private MainMenuState _mainMenuState;

    void Awake()
    {
        Init();
    }

    private void Start()
    {
        LateInit();
    }

    // Starts the game, not necessary a new game, for example like Resume from the Pause
    public void StartGame()
    {
        _pauseManager.SetPause(false);
        CurrentGameState = _runningGameState;
        GameStateSwitched.Invoke(CurrentGameState);
    }

    public void PauseGame()
    {
        _pauseManager.SetPause(true);
        CurrentGameState = _pausedGameState;
        GameStateSwitched.Invoke(CurrentGameState);
    }

    public void TogglePause()
    {
        if (CurrentGameState is RunningGameState)
            PauseGame();

        else if (CurrentGameState is PausedGameState)
            StartGame();
    }

    public void BackToMenu()
    {
        _pauseManager.SetPause(true);
        CurrentGameState = _mainMenuState;
        GameStateSwitched.Invoke(CurrentGameState);
    }

    public void ShowDeathScreen()
    {
        _pauseManager.SetPause(true);
        CurrentGameState = _deathScreenState;
        GameStateSwitched.Invoke(CurrentGameState);
    }

    public void SetupMenusForGameStates(List<Menu> menuList)
    {
        foreach (Menu menu in menuList)
        {
            if (menu is HudMenu)
                _runningGameState.SetMenu(menu);

            else if (menu is PauseMenu)
                _pausedGameState.SetMenu(menu);

            else if (menu is MainMenu)
                _mainMenuState.SetMenu(menu);

            else if (menu is DeathMenu)
                _deathScreenState.SetMenu(menu);
        }
    }

    private void Init()
    {
        // Init variables
        _playerInputManager = ProjectContext.Instance.PlayerInputManager;
        _pauseManager = ProjectContext.Instance.PauseManager;
        _player = ProjectContext.Instance.Player;

        _runningGameState = new RunningGameState();
        _pausedGameState = new PausedGameState();
        _deathScreenState = new DeathScreenState();
        _mainMenuState = new MainMenuState();

        CurrentGameState = _mainMenuState;
    }

    private void LateInit()
    {
        _pauseManager.SetPause(true);

        // Init events
        _playerInputManager.PausePressed += OnPausePressed;
        _player.PlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        ShowDeathScreen();
    }

    private void OnPausePressed()
    {
        TogglePause();
    }

    private void OnGameStateSwitched()
    {

    }
}
