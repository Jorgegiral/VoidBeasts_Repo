using Unity.VisualScripting;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject VFXexplosion;
    public Transform explosionTransform;
    public AudioClip placeMine;
    public AudioClip mineExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject tempExplosion = Instantiate(VFXexplosion,explosionTransform);
            Explosion explosionMine = tempExplosion.GetComponent<Explosion>();
            explosionMine.isMine = true;
            Destroy(tempExplosion,3f);
        }
    }

}
