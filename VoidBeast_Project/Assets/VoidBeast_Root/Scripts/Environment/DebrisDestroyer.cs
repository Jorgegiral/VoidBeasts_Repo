using Unity.VisualScripting;
using UnityEngine;

public class DebrisDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            if(other.gameObject.GetComponentInParent<TakeAreaEnviro>() != null)
            {
                TakeAreaEnviro unplace = other.gameObject.GetComponentInParent<TakeAreaEnviro>();
                unplace.Destroyed();
            }
            other.gameObject.SetActive(false);

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            if (other.gameObject.GetComponentInParent<TakeAreaEnviro>() != null)
            {
                TakeAreaEnviro unplace = other.gameObject.GetComponentInParent<TakeAreaEnviro>();
                unplace.Destroyed();
            }
            other.gameObject.SetActive(false);

        }

    }
}
