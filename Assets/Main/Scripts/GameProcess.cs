using UnityEngine;
using System;

[RequireComponent(typeof(PlayerInput))]
public class GameProcess : MonoBehaviour
{
    [Header("References")]
    private PlayerInput _playerInput;
    
    public GameState CurrentGameState;

    public Action GameStarted;
    public Action GameEnded;
    public Action GamePauseSwitched;

    void Start()
    {
        // Init variables
        CurrentGameState = GameState.Running;
        //IsPaused = false;
        _playerInput = gameObject.GetComponent<PlayerInput>();
        
        // Init events
        _playerInput.PausePressed += SwitchPause;
    }

    public void SwitchPause()
    {
        if (CurrentGameState == GameState.Paused)
        {
            Time.timeScale = 1;
            CurrentGameState = GameState.Running;
        }
        else
        {
            Time.timeScale = 0;
            CurrentGameState = GameState.Paused;
        }

        GamePauseSwitched.Invoke();
    }
}
