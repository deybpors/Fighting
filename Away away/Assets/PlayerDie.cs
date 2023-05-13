using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.position = spawnLocation.position;
        }
    }
}
