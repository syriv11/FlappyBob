using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action SpacePressed;
    public Action EscapePressed;

    public Action R_Pressed; // Just for test

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacePressed.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapePressed.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            R_Pressed.Invoke();
        }
    }
}
