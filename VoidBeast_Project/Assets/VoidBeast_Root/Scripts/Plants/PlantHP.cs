using System.Collections.Generic;
using UnityEngine;

public class PlantHP : MonoBehaviour
{
    [SerializeField] int plantHP;
    private void Awake()
    {

    }
    public void TakeDamage(int damage)
    {
        plantHP -= damage;
        if (plantHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}




