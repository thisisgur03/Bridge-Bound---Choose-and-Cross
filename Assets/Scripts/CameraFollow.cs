using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 offSet;

    private void LateUpdate()
    {
        transform.position = Target.position + offSet;
    }
}
