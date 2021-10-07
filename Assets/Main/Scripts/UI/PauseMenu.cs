using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameProcess _gameProcess;

    public void Resume()
    {
        _gameProcess.ResumeGame();
    }

    public void Restart()
    {
        Resume();
        _gameProcess.GameRestarted.Invoke();
    }
}
