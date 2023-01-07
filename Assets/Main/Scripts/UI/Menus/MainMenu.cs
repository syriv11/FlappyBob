using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    public void Play()
    {
        ProjectContext.Instance.GameProcess.StartGame();
    }
}
