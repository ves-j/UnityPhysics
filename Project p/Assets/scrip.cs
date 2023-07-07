using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float mouseSens = 100.0f;
    public float clampAngle = 80.0f;
    public float moveSpeed = 5.0f;

    private float rotX = 0.0f;
    private float rotY = 0.0f;

    public float jumpForce = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        rotX -= mouseY;
        rotY += mouseX;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        playerRigidbody.MoveRotation(Quaternion.Euler(0.0f, rotY, 0.0f));


        // Get movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.x *= moveSpeed;
        moveDirection.z *= moveSpeed;

        // Apply movement
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * Time.deltaTime);

        //Get Player X and Y
        float playerPosX = playerRigidbody.position.x;
        float playerPosY = playerRigidbody.position.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
