using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;
    
    
    //References
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection); //Makes forward the player forward

        if (isGrounded) //Only move when grounded
        {
         
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) //If we are moving and not presing leftShift
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection != Vector3.zero)
            {
                Idle();
            }
            moveDirection *= moveSpeed;
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump");
                Jump();
            }

        }
        

        //Apply gravity

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }
    private void Idle()
    {
        animator.SetFloat("Speed", 0);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f);
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    private IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }
}
