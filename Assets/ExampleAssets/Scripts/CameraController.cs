using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float smoothing = 5f;
	Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void LateUpdate()
	{
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
