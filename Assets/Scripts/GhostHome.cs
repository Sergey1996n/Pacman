using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    [field: SerializeField] public Transform Inside { get; private set; }
    [SerializeField] private Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        Ghost.Movement.SetDirection(Vector2.up, true);
        Ghost.Movement.Rigidbody.isKinematic = true;
        Ghost.Movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Ghost.SetPosition(Vector3.Lerp(position, Inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            Ghost.SetPosition(Vector3.Lerp(Inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        Ghost.Movement.Rigidbody.isKinematic = false;
        Ghost.Movement.enabled = true;
    }

}
