using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{

	public bool checkBounds;
	public Transform playerTransform;
	public float dampTime = 0.15f;
	public Vector3 minBoundPos;
	public Vector3 maxBoundPos;

	private Vector3 velocity = Vector3.zero;
	private Camera mCamera;


	void Awake()
	{
		this.mCamera = GetComponent<Camera>();
		this.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 point = this.mCamera.WorldToViewportPoint(this.playerTransform.position);
		Vector3 delta = this.playerTransform.position - mCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

		if (checkBounds)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minBoundPos.x, maxBoundPos.x),
				Mathf.Clamp(transform.position.y, minBoundPos.y, maxBoundPos.y),
				Mathf.Clamp(transform.position.z, minBoundPos.z, maxBoundPos.z)
			);
		}
	}
}