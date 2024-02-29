using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

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

    public void MovePlayer(Transform playerTransform, Vector3 normalizedDir, float speed)
    {
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();

        normalizedDir *= speed;

        rb.AddForce(normalizedDir, ForceMode.Force);
    }

    public void JumpPlayer(Transform playerTransform)
    {

    }
}
