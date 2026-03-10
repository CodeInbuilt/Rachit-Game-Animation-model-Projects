using UnityEngine;

public class playerGame : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private Transform feetpos;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private float crouchHeight = 0.5f;

    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;

    void Update()
    {
        // Ground Check
        isGrounded = Physics2D.OverlapCircle(feetpos.position, groundDistance, groundLayer);

        #region Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpTimer = 0f;
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        #endregion

        #region Crouch
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }
        #endregion

       void OnDrawGizmosSelected()
     {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(feetpos.position, groundDistance);
      }










    }
}