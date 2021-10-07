using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameProcess _gameProcess;

    [SerializeField] private List<GameObject> _menuList;

    private void Start()
    {
        _gameProcess.PauseSwitched += SwitchPauseMenu;
        _gameProcess.GameRestarted += SwitchStartMenu;
        SwitchPauseMenu();
    }

    private void SwitchPauseMenu()
    {
        _pauseMenu.SetActive(_gameProcess.IsPaused);
    }

    private void SwitchDeathMenu()
    {

    }

    private void SwitchStartMenu()
    {

    }
}
