using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField] private AnimatedSprite deathSequence;

    private Vector2 lastPosition;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private Movement movement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            movement.SetDirection(HandleTouchInput());
        }

        float angle = Mathf.Atan2(movement.Direction.y, movement.Direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private Vector2 HandleTouchInput()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastPosition = touch.position;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            return UpdateMovementDirection(touch.position - lastPosition);
        }

        return Vector2.zero;
    }

    private Vector2 UpdateMovementDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }
        else
        {
            if (delta.y > 0)
            {
                return Vector2.up;
            }
            else
            {
                return Vector2.down;
            }
        }
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.SpriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.SpriteRenderer.enabled = true;
        deathSequence.Restart();
    }

}
