using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameProcess _gameProcess;

    [SerializeField] private List<GameObject> _menuList;

    private void Start()
    {
        _gameProcess.GamePauseSwitched += SwitchPauseMenu;
        //_gameProcess.GameStarted += SwitchStartMenu;
    }

    private void SwitchPauseMenu()
    {
        _pauseMenu.SetActive(_gameProcess.CurrentGameState == GameState.Paused ? true : false);
    }

    private void SwitchDeathMenu()
    {

    }

    private void SwitchStartMenu()
    {

    }
}
