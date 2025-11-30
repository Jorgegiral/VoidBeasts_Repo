using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] LayerMask layerInteractable;
    [SerializeField] LayerMask layerPlant;
    [SerializeField] private float rayDistance = 6f;
    private GameObject lastSelected;

    void Update()
    {
        if (DayNightSystem.Instance.isDay && PlayerStats.instance.isActionMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerPlant | layerInteractable))
            {
                Transform selectionChild = hit.collider.transform.Find("Selection");

                if (selectionChild != null)
                {
                    if (lastSelected != null && lastSelected != selectionChild.gameObject)
                        lastSelected.SetActive(false);

                    selectionChild.gameObject.SetActive(true);
                    lastSelected = selectionChild.gameObject;
                }
            }
            else
            {
                if (lastSelected != null)
                {
                    lastSelected.SetActive(false);
                    lastSelected = null;
                }

            }

        }
        if (DayNightSystem.Instance.isNight) DayNightSystem.Instance.selection.SetActive(false);

    }
}