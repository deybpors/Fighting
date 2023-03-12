using UnityEngine;
using static ObjectPooler;

public class HitObject : MonoBehaviour
{
    public GameObject creator;

    public float despawnTime = .1f;

    private float currentTime;

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
            instance.ReturnObject(pool);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if(collision.gameObject == creator) return;

        Debug.Log("We hit " +  collision.gameObject);
        instance.ReturnObject(pool);
    }
}
