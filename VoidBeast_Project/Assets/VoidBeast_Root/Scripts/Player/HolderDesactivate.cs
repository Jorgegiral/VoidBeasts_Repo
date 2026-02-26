using UnityEngine;

public class HolderDesactivate : MonoBehaviour
{
    void Update()
    {
        if (DayNightSystem.Instance.isDay)
        {
            gameObject.SetActive(false);
        }
    }
}
