using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantHP : MonoBehaviour
{
    [SerializeField] float plantHP;
    [SerializeField] float maxPlantHP;
    [SerializeField] Parcela plantParcela;
    [SerializeField] private Image healthbar;
    private void Start()
    {
        plantHP = maxPlantHP;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage)
    {
        plantHP -= damage;
        UpdateHealthBar();
        if (plantHP <= 0)
        {
            plantParcela = GetComponentInParent<Parcela>();
            plantParcela.DestroyedPlant();
            Destroy(gameObject);
        }
    }
    void UpdateHealthBar()
    {
        float health = plantHP / maxPlantHP;
        health = Mathf.Clamp01(health);
        Color fullColor = new Color(0f, 1f, 0.6f);
        Color color;
        if (healthbar != null)
        {
            healthbar.fillAmount = health;
            if (health > 0.66f)
            {
                color = Color.Lerp(Color.yellow, fullColor, (health - 0.66f) / 0.34f);
            }
            else if (health > 0.33f)
            {
                color = Color.Lerp(Color.orange, Color.yellow, (health - 0.33f) / 0.33f);
            }
            else
            {
                color = Color.Lerp(Color.red, Color.orange, health / 0.33f);
            }
            healthbar.color = color;
        }
    }

}




