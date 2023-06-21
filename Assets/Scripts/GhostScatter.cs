using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !Ghost.Frightened.enabled)
        {
            int index = Random.Range(0, node.AvailableDirections.Count);

            if (node.AvailableDirections.Count > 1 && node.AvailableDirections[index] == -Ghost.Movement.Direction)
            {
                index++;

                if (index >= node.AvailableDirections.Count) {
                    index = 0;
                }
            }

            Ghost.Movement.SetDirection(node.AvailableDirections[index]);
        }
    }

}
