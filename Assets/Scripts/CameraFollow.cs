using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float followSpeed;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position + offSet;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
