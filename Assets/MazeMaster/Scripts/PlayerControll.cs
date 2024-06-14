using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] InputActionReference inputActionMove;
    [SerializeField] InputActionReference inputActionLook;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    Vector3 velocity;
    const float speed = 12f;
    const float groundDistance = 0.4f;
    const float gravity = -9.81f * 2;
    const float jumpHeight = 3f;
    bool isGrounded;
    Animator animator = null;
    public float mouseSensitivity;
    private float xRotation;

    [SerializeField]
    Transform PlayerCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GamePaused || GameManager.Instance.GameOver)
        {
            return;
        }
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //force player to the ground, better then 0
        }
        Vector2 movment = inputActionMove.action.ReadValue<Vector2>();
        if (movment.x != 0 || movment.y != 0)
        {
            WalkAnim(true);
        }
        else
        {
            WalkAnim(false);
        }
        Vector3 move = transform.right * movment.x + transform.forward * movment.y;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveCamera()
    {
        Vector2 gamepad = inputActionLook.action.ReadValue<Vector2>();
        Vector2 realMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float mouseX =  gamepad.x * mouseSensitivity * Time.deltaTime;
        float mouseY =  gamepad.y * mouseSensitivity * Time.deltaTime;
        if (gamepad == Vector2.zero)
        {
            mouseX =  realMouse.x * mouseSensitivity * Time.deltaTime;
            mouseY =  realMouse.y * mouseSensitivity * Time.deltaTime;
        }
        if (Settings.Invert)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        var objName = collision.gameObject.tag;
        if (objName.Contains("Score"))
        {
            PickUpAnim();
            GameManager.Instance.HitScore(collision.gameObject);
        }
        else if (objName.Contains("Keys"))
        {
            PickUpAnim();
            GameManager.Instance.HitKeys(collision.gameObject);
        }
        else if (objName.Contains("Zombie"))
        {
            GameManager.Instance.HitEnemy();
        }
        else if (objName.Contains("Door"))
        {
            GameManager.Instance.HitDoors(collision.gameObject);
        }
        else if (objName.Contains("Saw"))
        {
            GameManager.Instance.HitSaw();
        }
        else if (objName.Contains("Finish"))
        {
            PickUpAnim();
            GameManager.Instance.HitFinish(collision.gameObject);
        }
    }

    void PickUpAnim()
    {
        animator.Play("pickup");
    }

    void WalkAnim(bool play)
    {
        if (play)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("idle");
        }
    }
}
