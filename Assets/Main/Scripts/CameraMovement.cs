using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPosition;

    private Vector3 _offset;

    private void Start()
    {
        _offset = gameObject.transform.position - _playerPosition.position;
        //gameObject.transform.position
    }

    private void Update()
    {

        gameObject.transform.position = new Vector3(_playerPosition.position.x + _offset.x, _offset.y, _offset.z);
        //gameObject.transform.position = _offset;
        //gameObject.transform.Translate(new Vector3(_playerPosition.position.x, 0, 0));
        //gameObject.transform.position = new ;
    }
}
