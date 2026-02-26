using UnityEngine;

public class BuildLock : MonoBehaviour
{

    [SerializeField] GameObject cropUnavailable;
    [SerializeField] GameObject towerUnavailable;
    [SerializeField] GameObject wallUnavailable;

    void Update()
    {
        unavailable();
    }


    public void unavailable()
    {
        if (UpgradeManager.instance.cropAvailable == 0) cropUnavailable.SetActive(true);
        else cropUnavailable.SetActive(false);

        if (UpgradeManager.instance.towerAvailable == 0) towerUnavailable.SetActive(true);
        else towerUnavailable.SetActive(false);

        if (UpgradeManager.instance.wallAvailable == 0) wallUnavailable.SetActive(true);
        else wallUnavailable.SetActive(false);
    }
}
