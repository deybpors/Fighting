using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalMovement;
    private Transform trans;
    private Vector2 checkGround = Vector2.down;
    float distance = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        horizontalMovement = horizontalInput * moveSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        DebugRaycast();
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = movement;
      

        RaycastHit2D hit = Physics2D.Raycast(trans.position + offset, checkGround, distance, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void DebugRaycast()
    {
        Vector2 newPos = (trans.position + offset);
        Vector2 endPoint = newPos + (checkGround * distance);
        Debug.DrawLine(trans.position, endPoint, Color.green);
    }
}
