using UnityEngine;

public class DebrisDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            other.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
