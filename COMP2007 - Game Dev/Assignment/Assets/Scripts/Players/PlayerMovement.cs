using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 15f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    public Animator leftLeg;
    public Animator rightLeg;


    CharacterController characterController;
    void Start()
    {
        //Get the players character controller
        characterController = GetComponent<CharacterController>();
        //Set cursor to locked and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Get movement directions
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Check sprint button is pressed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX;
        float curSpeedY;

        //If the player is able to move
        if (canMove)
        {
            float speed;

            //Set speed to run speed if sprinting and walk speed if not
            if (isRunning)
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            //Get directional inputs
            float inputVertical = Input.GetAxis("Vertical");
            float inputHorizontal = Input.GetAxis("Horizontal");

            //Set directional speed based on inputs
            curSpeedX = speed * inputVertical;
            curSpeedY = speed * inputHorizontal;

            //If moving
            if(curSpeedX > 0 || curSpeedY > 0)
            {
                //If running
                if(isRunning)
                {
                    //Start running animations
                    leftLeg.SetBool("isRunning", true);
                    rightLeg.SetBool("isRunning", true);
                    leftLeg.SetBool("isWalking", false);
                    rightLeg.SetBool("isWalking", false);
                }
                else
                {
                    //Start walking animations
                    leftLeg.SetBool("isRunning", false);
                    rightLeg.SetBool("isRunning", false);
                    leftLeg.SetBool("isWalking", true);
                    rightLeg.SetBool("isWalking", true);
                }
            }
            else
            {
                //Stop all animations
                leftLeg.SetBool("isRunning", false);
                rightLeg.SetBool("isRunning", false);
                leftLeg.SetBool("isWalking", false);
                rightLeg.SetBool("isWalking", false);
            }
        }
        else
        {
            //Speed set to nothing
            curSpeedX = 0;
            curSpeedY = 0;

            //Stop all animations
            leftLeg.SetBool("isRunning", false);
            rightLeg.SetBool("isRunning", false);
            leftLeg.SetBool("isWalking", false);
            rightLeg.SetBool("isWalking", false);
        }

        //Set movement directions
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //If jump button pressed and player can jump add jump power to movement
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Add gravity if player is not on the floor
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Move player by the set move direction
        characterController.Move(moveDirection * Time.deltaTime);

        //If player can move
        if (canMove)
        {
            //Change rotation of the player based on mouse movement
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}