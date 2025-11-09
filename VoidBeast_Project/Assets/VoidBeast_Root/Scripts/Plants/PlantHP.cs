using System.Collections.Generic;
using UnityEngine;

public class PlantHP : MonoBehaviour
{
    [SerializeField] int plantHP;
    [SerializeField] Parcela plantParcela;
    private void Awake()
    {
        plantParcela = GetComponentInParent<Parcela>();
    }
    public void TakeDamage(int damage)
    {
        plantHP -= damage;
        if (plantHP <= 0)
        {
            plantParcela.DestroyedPlant();
            Destroy(gameObject);

        }
    }
}




