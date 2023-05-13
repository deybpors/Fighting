using System;
using System.Collections;
using UnityEngine;
using static ObjectPooler;

public class Hitter : MonoBehaviour
{
    [Range(0f, .5f)]
    [SerializeField] private float attackCastRate = .3f;
    [Range(0f, .5f)]
    [SerializeField] private float attackRate = .3f;
    [SerializeField] private Vector2 hitOffset = new Vector2(0, 1f);
    [SerializeField] private Pool hitObject;
    [SerializeField] private float basicHitKnockback = 3f;

    private float attackTimer;
    private int basicHitCombo;
    private float attackCastTimer;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public void BasicHitCombo()
    {
        if (attackTimer <= attackRate)
        {
            return;
        }

        float horizontalInput = controller.GetInput().MoveValue.x;
        if(horizontalInput == 0)
        {
            horizontalInput = transform.rotation.eulerAngles.y > 0 ? -1 : 1;
        }

        var inputVector = new Vector2(horizontalInput, controller.GetInput().MoveValue.y).normalized;
        inputVector += hitOffset;


        var hitObj = instance.GetObject(hitObject, (Vector2)transform.position + inputVector, Quaternion.identity);
        hitObj.prefab.transform.parent = transform;

        var hitterObj = hitObj.prefab.GetComponent<HitObject>();
        hitterObj.creator = gameObject;

        StartCoroutine(Pause());

        if (basicHitCombo >= 3)
        {
            hitterObj.InitializeKnockbackForce(basicHitKnockback * 2f);
            basicHitCombo = 0;
            return;
        }

        hitterObj.InitializeKnockbackForce(basicHitKnockback);
        basicHitCombo++;
    }

    private IEnumerator Pause()
    {
        attackTimer = 0;
        while(true)
        {
            controller.enabled = false;
            attackCastTimer += Time.deltaTime;
            if(attackCastTimer > attackCastRate)
            {
                controller.enabled = true;
                attackCastTimer = 0;
                break;
            }
            yield return null;
        }
    }

    public void ResetAttackTimer()
    {
        attackCastTimer = 0;
    }
}
