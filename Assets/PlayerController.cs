using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    public Vector3 direction = Vector3.zero;

    public float walkingSpeed;
    public float jumpForce;

    public float gravity = 9.81f;
    public float gravityForce;

    public bool isGrounded = false;

    public float cameraAngleY;
    public Camera MyCamera;
    public float mouseXSensibility = 1.0f;
    public float mouseYSensibility = 1.0f;

    public bool invertY = false;

    public float topAngleY = 45.0f;
    public float botAngleY = -45.0f;

    private Rigidbody rigidBody;

    public bool isDead;
    public int health;
    public int shield;
    public int ammo;
    public int savedAmmo;
    public bool haskey = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
     
    }
    void FixedUpdate()
    {
        float dir_x = Input.GetAxis("Horizontal");
        float dir_z = Input.GetAxis("Vertical");

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        int invert = invertY ? -1 : 1;

        cameraAngleY += mouse_Y * mouseYSensibility * invert;
        cameraAngleY = Mathf.Clamp(cameraAngleY, botAngleY, topAngleY);

        Quaternion angle_mouseX = Quaternion.Euler(0.0f, mouse_X * mouseXSensibility, 0.0f);
        Quaternion angle_mouseY = Quaternion.Euler(cameraAngleY, 0.0f, 0.0f);

        transform.localRotation *= angle_mouseX;
        MyCamera.transform.localRotation = angle_mouseY;

        //player controls

        float runMultiplier = (Input.GetAxis("Run") > 0) ? 2.0f : 1.0f;

        direction.x = dir_x * walkingSpeed * runMultiplier;
        direction.z = dir_z * walkingSpeed * runMultiplier;

        direction.y = -gravity * gravityForce;

        direction = Quaternion.FromToRotation(Vector3.forward, transform.forward) * direction;

        player.Move(direction * Time.deltaTime);

        if ((Input.GetButtonDown("Jump")) && (isGrounded))
        {
            direction.y = jumpForce;
            if (jumpForce > 0.0f)
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        isGrounded = player.isGrounded;
        
        if (!isGrounded)
        {
            direction.y -= gravity * gravityForce * Time.deltaTime;
        }

        if (Input.GetAxis("Cancel") > 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
