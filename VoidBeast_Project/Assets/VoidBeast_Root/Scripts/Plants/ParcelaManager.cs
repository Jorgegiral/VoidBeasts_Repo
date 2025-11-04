using UnityEngine;

public class ParcelaManager : MonoBehaviour
{
    [SerializeField] Parcela[] parcelas;
    public static ParcelaManager instance;

    void Awake()
    {
        if (instance == null) { instance = this; }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
