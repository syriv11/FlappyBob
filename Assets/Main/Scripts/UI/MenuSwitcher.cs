using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    private List<Menu> _menuList;

    private GameProcess _gameProcess;

    private void Start()
    {
        Init();
        SetupMenuList();

        _gameProcess.SetupMenusForGameStates(_menuList);
        OpenMenu(_gameProcess.CurrentGameState.Menu);
    }

    private void Init()
    {
        // Init variables
        _gameProcess = ProjectContext.Instance.GameProcess;
        _menuList = new List<Menu>();

        // Init events
        _gameProcess.GameStateSwitched += OnGameStateSwitched;
    }

    private void SetupMenuList()
    {
        foreach (Transform child in gameObject.transform)
        {
            _menuList.Add(child.gameObject.GetComponent<Menu>());
        }
    }

    private void OpenMenu(Menu menu)
    {
        DisableAllMenus();
        menu.SetMenuVisibility(true);
    }

    private void DisableAllMenus()
    {
        foreach (var menu in _menuList)
        {
            menu.SetMenuVisibility(false);
        }
    }

    private void OnGameStateSwitched(GameState newGameState)
    {
        OpenMenu(newGameState.Menu);
    }
}
