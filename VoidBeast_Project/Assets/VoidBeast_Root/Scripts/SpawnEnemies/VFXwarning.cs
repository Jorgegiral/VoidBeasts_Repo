using UnityEngine;

public class VFXwarning : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float speed;
    private float angle;
    private Quaternion initialRotation;
    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = player.position;

        int segments = 64;
        float step = 2 * Mathf.PI / segments;
        Vector3 prev = center + new Vector3(Mathf.Cos(0) * radius, 0, Mathf.Sin(0) * radius);

        for (int i = 1; i <= segments; i++)
        {
            float a = i * step;
            Vector3 next = center + new Vector3(Mathf.Cos(a) * radius, 0, Mathf.Sin(a) * radius);
            Gizmos.DrawLine(prev, next);
            prev = next;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(center, transform.position);
    }
}
