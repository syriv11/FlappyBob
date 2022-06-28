using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D _rigidbody;

    [Header("Parameters")]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _movementSpeed;

    public Vector3 StartPosition { get; private set; }

    private void Start()
    {
        // Init variables
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        StartPosition = gameObject.transform.position;
    }

    public void Jump()
    {
        if (_rigidbody.simulated)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            _rigidbody.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Force);
        }
    }

    public void Move()
    {
        if (_rigidbody.simulated)
        {
            _rigidbody.velocity = new Vector2(_movementSpeed, _rigidbody.velocity.y);
            SetRotation();
        }
    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity)
            * Quaternion.FromToRotation(Vector3.up, Vector3.right)
            * Quaternion.FromToRotation(Vector3.back, Vector3.up);
        Debug.DrawLine(transform.position, transform.position + (Vector3)_rigidbody.velocity.normalized, new Color(255, 0, 0));
    }

    public void ResetPosition()
    {
        _player.transform.position = StartPosition;
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetSimulation(bool isSimulated)
    {
        _rigidbody.simulated = isSimulated;
    }
}
