using UnityEngine;

public class BuildingHP : MonoBehaviour
{
    [SerializeField] int buildHP;
    [SerializeField] Canvas deathCanvas;

    private void Awake()
    {
        deathCanvas.gameObject.SetActive(false);

    }
    void TakeDamage(int damage)
    {
        buildHP -= damage;
        if (buildHP < 0 )
        {
            deathCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}

