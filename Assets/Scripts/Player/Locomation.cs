using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomation
{
    //camera movement
    private float xRot;

    //player movment
    private float smoothTime = 0.1f;
    private Vector3 vel = Vector3.zero;
    private Vector3 targetVel = Vector3.zero;

    //player Jump
    private float jumpSpeed = 5f;
    private float offset = 0.9f;
    private bool isGrounded;
    private float groundedDist = 0.2f;
    //private bool isJumping;
    //private float jumpStartTime;
    //private float jumpDuration = 1f;

    public void MoveCamera(Transform playerTransform, Transform cameraTransform)
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * DataManager.SenX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * DataManager.SenY;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    public void MovePlayer(Transform playerTransform, float speed)
    {
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        //inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        //the camera look direction
        Vector3 forward = playerTransform.forward;
        Vector3 right = playerTransform.right;
        forward.Normalize();
        right.Normalize();
        forward.y = 0;
        right.y = 0;

        Vector3 dir = (forward * moveZ + right * moveX).normalized * speed;

        targetVel = Vector3.SmoothDamp(targetVel, dir, ref vel, smoothTime);

        targetVel.y = rb.velocity.y;

        rb.velocity = targetVel;
    }

    public void JumpPlayer(PlayerController playerCtrl, Transform playerTransform)
    {
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        int mask = LayerMask.GetMask("Ground");
        Vector3 offPos = playerTransform.transform.position + Vector3.down * offset;
        isGrounded = Physics.Raycast(offPos, Vector3.down, groundedDist, mask, QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") && isGrounded /*&& !isJumping*/) //<--- checks if its grouded, during a jump and if you pressed "jump"
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse); //<--- temporary jump
            //isJumping = true;
            //jumpStartTime = Time.time
        }

        //if (isJumping)
        //{
        //    float elapsed = Time.time - jumpStartTime;
        //    if (elapsed < jumpDuration)
        //    {
        //        float jumpProgress = elapsed / jumpDuration;
        //        float jumpForce = playerCtrl.GetJumpCurve().Evaluate(jumpProgress) * jumpSpeed;
        //        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //    }
        //    else
        //    {
        //        isJumping = false;
        //    }
        //}
    }
}
