using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float animationTime = 0.25f;
    [SerializeField] private bool loop = true;
    private int animationFrame;
    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!SpriteRenderer.enabled) {
            return;
        }

        animationFrame++;

        if (animationFrame >= sprites.Length && loop) {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) {
            SpriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart()
    {
        animationFrame = -1;

        Advance();
    }

}
