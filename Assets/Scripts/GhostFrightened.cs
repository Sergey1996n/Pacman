using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer eyes;
    [SerializeField] private SpriteRenderer blue;
    [SerializeField] private SpriteRenderer white;

    private bool eaten;

    private void Eaten()
    {
        eaten = true;
        Ghost.SetPosition(Ghost.Home.Inside.position);
        Ghost.Home.Enable(Duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        Ghost.Movement.SpeedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        Ghost.Movement.SpeedMultiplier = 1f;
        eaten = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.AvailableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (Ghost.Target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled) {
                Eaten();
            }
        }
    }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
}
