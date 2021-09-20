using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action SpacePressed;

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacePressed.Invoke();
        }
    }
}
