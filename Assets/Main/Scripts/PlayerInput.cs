using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action SpacePressed;
    public Action EscapePressed;

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
    }
}
