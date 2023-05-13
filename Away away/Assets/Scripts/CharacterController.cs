using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalMovement;
    private Transform trans;
    private Quaternion left;
    private Quaternion right;
    private CustomInput input;
    private Collider2D col;
    private Collision2D cols;
    private Hitter hitter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = transform;
        input = GetComponent<CustomInput>();
        col = GetComponent<Collider2D>();
        hitter = GetComponent<Hitter>();
        left = Quaternion.Euler(0, 180, 0);
        right = Quaternion.Euler(0, 0, 0);


        if (input != null)
        {
            input.OnJump += Jump;
            input.OnHit += hitter.BasicHitCombo;
            input.OnHit += hitter.ResetAttackTimer;
        }
    }

    void Update()
    {
        horizontalMovement = input.MoveValue.x * moveSpeed;

        if(input.MoveValue.x < 0)
        {
            trans.rotation = left;
        }
        else if(input.MoveValue.x > 0)
        {
            trans.rotation = right;
        }

        // Apply different forces during ascent and descent
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !input.JumpValue)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (!isGrounded) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void FixedUpdate()
    {
        if (horizontalMovement == 0) return;
        Vector2 movement = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = movement;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        cols = collision;
        // Get the contact point of the collision.
        Vector2 contactPoint = collision.GetContact(0).point;

        // Check if the contact point is below the center of the collider.
        isGrounded = contactPoint.y < col.bounds.center.y;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(cols == collision)
        {
            cols = null;
            isGrounded = false;
        }
    }

    private void OnDisable()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public CustomInput GetInput()
    {
        return input;
    }

}
