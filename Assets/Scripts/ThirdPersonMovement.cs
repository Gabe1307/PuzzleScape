using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float speed = 6f;
    public float sprintSpeed = 10f;
    private float currentSpeed;

    public float jumpHeight = 0.3f;
    private bool isJumping = false;

    Animator anim;

    private void Start()
    {
        currentSpeed = speed; // Set the initial speed
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Check for sprint input and update speed accordingly
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            anim.SetFloat("Blend", 1f);
        }
        else
        {
            currentSpeed = speed;
        }

        if (controller.isGrounded)
        {
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
           transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
           controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
            controller.Move(Vector3.down * Time.deltaTime * 10);

            if(currentSpeed == speed)
            {
                anim.SetFloat("Blend", 0.5f);
            }
        }
        if(direction.magnitude < 0.1f)
        {
            anim.SetFloat("Blend", 0f);
        }

    }
}

