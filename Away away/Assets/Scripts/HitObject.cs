using Photon.Pun;
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

        Debug.Log(gameObject.name + " hit " + objectHit.name + "\nwith a force of " + knockbackForce);

        var view = objectHit.GetComponent<PhotonView>();
        if (view != null)
        {
            var force = (objectHit.transform.position - creator.transform.position).normalized * knockbackForce;
            force.y = knockbackForce;
            view.RPC("KnockbackObject", RpcTarget.All, (Vector2)force);
        }

        if(pool == null || pool.prefab == null)
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
