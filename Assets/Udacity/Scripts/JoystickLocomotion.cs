using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickLocomotion : MonoBehaviour
{
    // public Rigidbody player;
    Vector3 directionToMove;
    float gravity;
    CharacterController characterController;

    Transform mainCameraTransform;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        directionToMove = Vector3.zero;
        gravity = -9.8f;
        characterController = this.GetComponent<CharacterController>();

        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController.isGrounded)
        {
            directionToMove.y += gravity * Time.deltaTime;
        }
        else
        {
            if (mainCameraTransform.eulerAngles.x >= 15f && mainCameraTransform.eulerAngles.x <= 30f)
            {
                directionToMove = mainCameraTransform.TransformDirection(Vector3.forward);
                directionToMove = directionToMove * speed;
            }
            else
            {
                directionToMove = Vector3.zero;

            }

        }

        characterController.Move(directionToMove * Time.deltaTime);
    }

    
}
