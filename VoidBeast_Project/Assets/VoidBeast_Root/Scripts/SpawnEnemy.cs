using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private bool nightStarted;
    [SerializeField] object enemy;
    [SerializeField] Transform spawner;
    private int enemyQuantity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nightStarted)
        {
           // for()
           // Instantiate(enemy, spawner,Quaternion );
            nightStarted = false;
        }
    }
}
