using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Controls { mobile, pc }

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    // Dash system
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTimer;

    private Rigidbody2D rb;
    private bool isGroundedBool = false;
    private bool canDoubleJump = false;

    public Animator playeranim;
    public Controls controlmode;

    private float moveX;
    public bool isPaused = false;

    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmissions;

    public ParticleSystem ImpactEffect;
    private bool wasonGround;

    // public GameObject projectile;
    // public Transform firePoint;

    public float fireRate = 0.5f; // Time between each shot
    private float nextFireTime = 0f; // Time of the next allowed shot

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footEmissions = footsteps.emission;

        if (controlmode == Controls.mobile)
        {
            UIManager.instance.EnableMobileControls();
        }
    }

    private void Update()
    {
        // Handle dash cooldown
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }

        // Handle dash duration
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }

        if (!isDashing)
        {
            isGroundedBool = IsGrounded();

            if (isGroundedBool)
            {
                canDoubleJump = true; // Reset double jump when grounded

                if (controlmode == Controls.pc)
                {
                    moveX = Input.GetAxis("Horizontal");
                }

                if (Input.GetButtonDown("Jump"))
                {
                    Jump(jumpForce);
                }
            }
            else
            {
                if (canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    Jump(doubleJumpForce);
                    canDoubleJump = false; // Disable double jump until grounded again
                }
            }

            if (!isPaused)
            {
                // Calculate rotation angle based on mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 lookDirection = mousePosition - transform.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

                // Handle shooting
                if (controlmode == Controls.pc && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate; // Set the next allowed fire time
                }

                // Handle dash (PC)
                if (controlmode == Controls.pc && Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                {
                    StartDash();
                }
            }

            SetAnimations();

            if (moveX != 0)
            {
                FlipSprite(moveX);
            }
        }

        //impactEffect
        if (!wasonGround && isGroundedBool)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x, footsteps.transform.position.y - 0.2f);
            ImpactEffect.Play();
        }

        wasonGround = isGroundedBool;
    }

    public void SetAnimations()
    {
        if (moveX != 0 && isGroundedBool && !isDashing)
        {
            playeranim.SetBool("run", true);
            footEmissions.rateOverTime = 35f;
        }
        else
        {
            playeranim.SetBool("run", false);
            footEmissions.rateOverTime = 0f;
        }

        playeranim.SetBool("isGrounded", isGroundedBool);
        playeranim.SetBool("isDashing", isDashing);
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            // Moving right, flip sprite to the right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            // Moving left, flip sprite to the left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            // During dash, maintain dash velocity
            float dashDirection = transform.localScale.x > 0 ? 1 : -1;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Normal player movement
            if (controlmode == Controls.pc)
            {
                moveX = Input.GetAxis("Horizontal");
            }

            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zero out vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playeranim.SetTrigger("jump");
    }

    private void StartDash()
    {
        if (canDash && !isDashing)
        {
            isDashing = true;
            canDash = false;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;

            // Trigger dash animation if you have one
            playeranim.SetTrigger("dash");

            // Optional: Add dash effect
            GameManager.instance.TriggerScreenShake(0.1f, 0.2f);
        }
    }

    private bool IsGrounded()
    {
        float rayLength = 0.25f;
        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "killzone")
        {
            GameManager.instance.Death();
        }
    }

    //mobile controls
    public void MobileMove(float value)
    {
        moveX = value;
    }

    public void MobileJump()
    {
        if (isGroundedBool)
        {
            // Perform initial jump
            Jump(jumpForce);
        }
        else
        {
            // Perform double jump if allowed
            if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false; // Disable double jump until grounded again
            }
        }
    }

    public void MobileDash()
    {
        if (canDash && !isDashing)
        {
            StartDash();
        }
    }

    public void Shoot()
    {
        //GameObject fireBall = Instantiate(projectile, firePoint.position, Quaternion.identity);
        //fireBall.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 500f);
    }

    public void MobileShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Set the next allowed fire time
        }
    }

    // Getters for other scripts
    public bool CanDash()
    {
        return canDash;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public float GetDashCooldownPercent()
    {
        if (canDash) return 1f;
        return 1f - (dashCooldownTimer / dashCooldown);
    }
}