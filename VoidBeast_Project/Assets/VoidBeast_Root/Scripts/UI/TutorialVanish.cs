using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialVanish : MonoBehaviour
{
    [SerializeField] private float vanishTime = 5f;
    [SerializeField] GameObject freeSeedText;
    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(VanishAfterTime());
    }
    private void Update()
    {
        if (PlayerStats.instance.menuOpened)
        {
            gameObject.SetActive (false);
        }
    }
    private IEnumerator VanishAfterTime()
    {
        yield return new WaitForSeconds(vanishTime);
        gameObject.SetActive(false);
        freeSeedText.gameObject.SetActive(true);
    }
}
