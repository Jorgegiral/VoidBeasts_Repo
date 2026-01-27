using System.Collections;
using UnityEngine;

public class DetectRadar : MonoBehaviour
{
    public bool active;

    void Update()
    {
        if (active)
        {
            gameObject.SetActive(true);
            StartCoroutine(TimeTillVanish());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TimeTillVanish()
    {
        yield return new WaitForSeconds(1);
        active = false;
    }
}
