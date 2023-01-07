using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : Menu
{
    public void BackToMenu()
    {
        ProjectContext.Instance.GameProcess.BackToMenu();
    }
}
