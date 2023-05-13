using UnityEngine;
using static ObjectPooler;

public class HitObject : MonoBehaviour
{
    public GameObject creator;
    public float despawnTime = .1f;
    private float currentTime;
    private float knockbackForce = 3f;

    private Pool pool;

    private void Start()
    {
        pool = new Pool(gameObject);
    }

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > despawnTime )
        {
            if (pool == null || pool.prefab == null)
            {
                pool = new Pool(gameObject);
            }
            instance.ReturnObject(pool);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        var objectHit = collision.gameObject;
        if (objectHit == creator) return;

        Debug.Log(creator.gameObject.name + " hit " + objectHit.name + "\nwith a force of " + knockbackForce);

        var force = (objectHit.transform.position - creator.transform.position).normalized * knockbackForce;
        force.y = knockbackForce;
        objectHit.SendMessage("KnockbackObject", (Vector2)force, SendMessageOptions.DontRequireReceiver);

        if (pool == null || pool.prefab == null)
        {
            pool = new Pool(gameObject);
        }
        instance.ReturnObject(pool);
    }

    public void InitializeKnockbackForce(float force)
    {
        knockbackForce = force;
    }
}
