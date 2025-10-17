using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFunctions : MonoBehaviour
{
    [Header("Shoot config")]
    [SerializeField] private float shootForce = 20f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletVFX;

    LayerMask layerInteractable;
    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);

    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");


    }
    void Shoot()
    {
        if (bulletVFX != null && shootPoint != null)
        {
            GameObject bullet = Instantiate(bulletVFX, shootPoint.position, shootPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootPoint.forward * shootForce;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - originRaycast, transform.TransformDirection(Vector3.forward), out hit, 30, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();
            }
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Shoot();
    }
}

