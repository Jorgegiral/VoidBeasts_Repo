using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] private int damage = 10;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed;
    [SerializeField] float fireRate;
    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        if(shootSpeed != 0)
        {
            transform.position += transform.forward * (shootSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

            GameObject hit = Instantiate(hitVFX, transform.position, transform.rotation);
            
            Destroy(gameObject);
        
    }
}
