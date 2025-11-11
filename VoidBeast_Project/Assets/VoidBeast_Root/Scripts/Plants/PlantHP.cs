using System.Collections.Generic;
using UnityEngine;

public class PlantHP : MonoBehaviour
{
    [SerializeField] float plantHP;
    [SerializeField] Parcela plantParcela;

    public void TakeDamage(float damage)
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




