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
    private Vector2 previousMoveInput;
    private Animator anim; //Jorge
    [SerializeField] private RotateToPlayer rotateToPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); //Jorge
        rb.freezeRotation = true;
        cameraFollowTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {

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
        bool wasMoving = previousMoveInput != Vector2.zero;
        bool isMoving = moveInput != Vector2.zero;

        if (wasMoving && !isMoving)
        {
            if (rotateToPlayer != null)
                rotateToPlayer.RotateOnStopMoving();
        }
        else if (!wasMoving && isMoving)
        {
            if (rotateToPlayer != null)
                rotateToPlayer.StopRotation();
        }
        anim.SetBool("isRunning", isMoving); //Jorge

        previousMoveInput = moveInput;
    
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;
        if (PlayerStats.instance.isPlanting) return;

        moveInput = context.ReadValue<Vector2>();
        
    }
}
