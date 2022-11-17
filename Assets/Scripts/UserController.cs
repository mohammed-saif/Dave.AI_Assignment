using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{

    public float speed = 2;
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private Animator userAnim;

    void Start()
    {
        userAnim = GetComponent<Animator>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if(direction.magnitude >= 0.1f)
        {
            userAnim.SetBool("IsMoving_b", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }
        else
        {
            userAnim.SetBool("IsMoving_b", false);
        }

    }
}
