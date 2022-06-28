using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action JumpPressed;
    public Action PausePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            JumpPressed.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePressed.Invoke();
        }
    }
}
