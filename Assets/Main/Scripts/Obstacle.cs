using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { Top, Buttom }

public class Obstacle : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector2 _size;
    [SerializeField] private Orientation _orientation;

    public Obstacle(Vector2 size, Orientation orientation) // Или все же вместо этого Start использовать?
    {
        _size = size;
        _orientation = orientation;
    }

    private void Start()
    {

    }
}
