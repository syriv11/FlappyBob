using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { Top, Buttom }

public class Obstacle : MonoBehaviour
{
    [Header("Parameters")]
    private float _gapHeight; // In range (-1, 1)
    private float _gapSize;

    private void Start()
    {

    }

    public void InitRandom()
    {

    }
}
