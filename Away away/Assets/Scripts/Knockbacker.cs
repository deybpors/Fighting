using UnityEngine;
using System.Collections;

public class Knockbacker : MonoBehaviour
{
    private Rigidbody2D rb;
    private CharacterController controller;
    private float timer = .4f;
    private float currentTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
    }

    public void KnockbackObject(Vector2 force)
    {
        if (rb == null) return;
        if(controller != null)
        {
            StartCoroutine(TurnOffControls());
        }
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator TurnOffControls()
    {
        while (true)
        {
            controller.enabled = false;
            currentTime += Time.deltaTime;
            if(currentTime > timer)
            {
                controller.enabled = true;
                currentTime = 0;
                break;
            }
            yield return null;
        }
    }
}
