using Unity.VisualScripting;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject VFXexplosion;
    public Transform explosionTransform;
    public AudioClip mineExplosion;

    private void Start()
    {
        Destroy(gameObject,15f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        GameObject tempExplosion = Instantiate(VFXexplosion, explosionTransform.position, explosionTransform.rotation);

        Explosion explosionMine = tempExplosion.GetComponent<Explosion>();
        Settings.instance.PlaySoundFXClip(mineExplosion, transform, 1f);
        Destroy(tempExplosion, 3f);
        Destroy(gameObject);

    }

}
