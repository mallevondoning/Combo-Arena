using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomation
{
    //camera movement
    private float yRot;

    //player movment

    //player Jump

    public void MoveCamera(Transform playerTransform, Transform cameraTransform)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * DataManager.senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * DataManager.senY;

        yRot -= mouseY;
        yRot = Mathf.Clamp(yRot, -90f, 90f);

        playerTransform.Rotate(Vector3.up * mouseX);
        cameraTransform.localRotation = Quaternion.Euler(yRot, 0, 0);
    }

    public void MovePlayer(Transform playerTransform, float speed)
    {
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 dir = playerTransform.forward * moveZ + playerTransform.right * moveX;

        dir = dir.normalized;
        dir *= speed;

        rb.AddForce(dir, ForceMode.Force);
    }

    public void JumpPlayer(Transform playerTransform)
    {

    }
}
