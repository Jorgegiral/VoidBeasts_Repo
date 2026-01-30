using UnityEngine;

public class Granada : MonoBehaviour
{
    [Header("Sounds and VFX")]
    public GameObject VFXexplosion;
    public AudioClip initialSound;
    public AudioClip bombExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Plant") && !other.gameObject.CompareTag("Confiner") && !other.gameObject.CompareTag("Player"))
        {
            GameObject tempExplosion = Instantiate(VFXexplosion, transform.position, transform.rotation);

            Explosion explosionMine = tempExplosion.GetComponent<Explosion>();
            if (explosionMine != null)
                explosionMine.isMine = false;

            Destroy(tempExplosion, 3f);
            Destroy(gameObject);
        }
    }
}
