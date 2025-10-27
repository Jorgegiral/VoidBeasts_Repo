using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToPlace;
    public float gridSize = 1f;
    private GameObject previewObject;
    private HashSet<Vector3> positionFilled = new HashSet<Vector3>();

    private void Start()
    {
        CreatePreviewObject();
    }
    private void Update()
    {
        UpdatePreviewPosition();
        if (Input.GetMouseButtonDown(0)) 
        {
            PlaceObject();
        }
    }
    void CreatePreviewObject()
    {
        previewObject=Instantiate(objectToPlace);
        previewObject.GetComponent<Collider>().enabled = false;
        Renderer[] renderers=previewObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine .Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("Zwrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }
    void UpdatePreviewPosition()
    {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask groundMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3 point = hit.point;
            Vector3 snappedPosition = new Vector3(Mathf.Round(point.x/gridSize)*gridSize, Mathf.Round(point.y/gridSize)*gridSize, Mathf.Round(point.z/gridSize)*gridSize);
            previewObject.transform.position = snappedPosition;
            if(positionFilled.Contains(snappedPosition))
            {
                SetColorPreview(Color.red);
            }
            else
            {
                SetColorPreview(Color.blue);
            }

        }
    }
    void SetColorPreview(Color color)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            mat.color = color;
        }
    }
    void PlaceObject()
    {
        Vector3 placementPostion = previewObject.transform.position;    
        if(!positionFilled.Contains(placementPostion))
        {
            Instantiate(objectToPlace,placementPostion,Quaternion.identity);
            positionFilled.Add(placementPostion);
        }
    }
}


