using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameProcess _gameProcess;

    public void Resume()
    {
        _gameProcess.SwitchPause();
    }

    public void Restart()
    {
        //Resume();
        _gameProcess.GameEnded.Invoke();
    }
}
