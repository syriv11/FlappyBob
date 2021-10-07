using UnityEngine;
using System;

public class GameProcess : MonoBehaviour
{
    private PlayerInput _playerInput;

    public Action PauseSwitched;
    public Action GameRestarted;

    public bool IsPaused { get; private set; }

    void Start()
    {
        IsPaused = false;

        // Init variables
        _playerInput = gameObject.GetComponent<PlayerInput>();
        
        // Init events
        _playerInput.EscapePressed += SwitchPause;
    }

    public void SwitchPause()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        // If call PauseGame happend but game is already in pause then do nothing. 
        // Else truly pause game and invoke event PauseSwitched
        if (!IsPaused)
        {
            Time.timeScale = 0;
            IsPaused = true;
            PauseSwitched.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
            PauseSwitched.Invoke();
        }
    }
}
