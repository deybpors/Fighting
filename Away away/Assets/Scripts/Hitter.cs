using UnityEngine;
using static ObjectPooler;

public class Hitter : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Pool hitObject;

    private Rigidbody2D rb;
    private Vector2 left = Vector2.left;
    private Vector2 right = Vector2.right;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.x < 0)
        {
            attackPoint.localPosition = left;
        }
        else
        {
            attackPoint.localPosition = right;
        }

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            var hitObj = instance.GetObject(hitObject, attackPoint.position, Quaternion.identity);
            hitObj.prefab.transform.parent = attackPoint;

            hitObj.prefab.GetComponent<HitObject>().creator = gameObject;
        }
    }
}
