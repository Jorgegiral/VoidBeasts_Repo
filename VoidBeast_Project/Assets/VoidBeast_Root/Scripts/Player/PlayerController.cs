using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float gravityValue = -9.81f;
    private Transform cameraFollowTransform;
    private Rigidbody rb;
    private Vector2 moveInput;

    private Animator anim; //Jorge

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); //Jorge
        rb.freezeRotation = true;
        cameraFollowTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {

        if (PlayerStats.instance.blockMovement)
        {
            rb.linearVelocity = Vector3.zero; 
            anim.SetBool("isRunning", false); 
            return; 
        }
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = cameraFollowTransform.forward * move.z + cameraFollowTransform.right * move.x;
        move.y = 0;

            rb.linearVelocity = new Vector3(move.x * PlayerStats.instance.playerSpeed, rb.linearVelocity.y + gravityValue * Time.fixedDeltaTime, move.z * PlayerStats.instance.playerSpeed);

        if (moveInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cameraFollowTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        }
        bool isMoving = moveInput != Vector2.zero;

 
        anim.SetBool("isRunning", isMoving); //Jorge
        if (PlayerStats.instance.blockMovement) anim.SetBool("isRunning", false);


    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;
        if (PlayerStats.instance.blockMovement) //Jorge

        return;

        moveInput = context.ReadValue<Vector2>();
        
    }
}
