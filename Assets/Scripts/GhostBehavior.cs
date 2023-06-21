using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    [field: SerializeField] public float Duration { get; private set; }
    public Ghost Ghost { get; private set; }

    private void Awake()
    {
        Ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
        Enable(Duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }

}
