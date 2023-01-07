using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    private GameProcess _gameProcess;

    private void Start()
    {
        _gameProcess = ProjectContext.Instance.GameProcess;
    }

    public void Resume()
    {
        _gameProcess.StartGame();
    }

    public void Restart()
    {
        _gameProcess.BackToMenu();
    }
}
