using System.Collections.Generic;
using UnityEngine;

public class PlantHP : MonoBehaviour
{
    [SerializeField] int plantHP;
    [SerializeField] Parcela plantParcela;

    public void TakeDamage(int damage)
    {
        plantHP -= damage;
        if (plantHP <= 0)
        {
            plantParcela = GetComponentInParent<Parcela>();
            plantParcela.DestroyedPlant();
            Destroy(gameObject);

        }
    }
}




