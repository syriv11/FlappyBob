using UnityEngine;
using System;

public class GameProcess : MonoBehaviour
{
    private PlayerInput _playerInput;

    public Action GamePaused;
    public Action GameStarted;
    public Action GameRestarted;

    public bool IsPaused { get; private set; }

    void Start()
    {
        IsPaused = false;

        // Init variables
        _playerInput = gameObject.GetComponent<PlayerInput>();
        
        // Init events
        _playerInput.EscapePressed += SwitchPause;
        _playerInput.R_Pressed += GameRestarted.Invoke;
        GamePaused += PauseGame;
        GameStarted += ResumeGame;
    }

    private void SwitchPause()
    {
        if (IsPaused)
        {
            GamePaused.Invoke();
        }
        else
        {
            GameStarted.Invoke();
        }
        IsPaused = !IsPaused;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
