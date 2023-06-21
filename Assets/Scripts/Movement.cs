using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private Vector2 initialDirection;
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 nextDirection;
    private Vector3 startingPosition;
    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 Direction { get; private set; }
    public float SpeedMultiplier { get; set; } = 1f;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        SpeedMultiplier = 1f;
        Direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        Rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (nextDirection != Vector2.zero) {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody.position;
        Vector2 translation = Direction * speed * SpeedMultiplier * Time.fixedDeltaTime;

        Rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (direction == Vector2.zero)
        {
            return;
        }

        if (forced || !Occupied(direction))
        {
            this.Direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

}
