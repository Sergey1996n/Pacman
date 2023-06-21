using UnityEngine;

public class PowerPellet : Pellet
{
    [field: SerializeField] public float Duration { get; protected set; } = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }

}
