using UnityEngine;

public class Granada : MonoBehaviour
{
    [Header("Sounds and VFX")]
    public GameObject VFXexplosion;
    public AudioClip bombExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Plant") && !other.gameObject.CompareTag("Confiner") && !other.gameObject.CompareTag("Player"))
        {
            GameObject tempExplosion = Instantiate(VFXexplosion, transform.position, transform.rotation);

            Explosion explosionMine = tempExplosion.GetComponent<Explosion>();
            Settings.instance.PlaySoundFXClip(bombExplosion, transform, 1f);

            Destroy(tempExplosion, 3f);
            Destroy(gameObject);
        }
    }
}
