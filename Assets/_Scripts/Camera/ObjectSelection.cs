using UnityEngine;
using System.Collections;

public class ObjectSelection : MonoBehaviour {

	RaycastHit hit;
	
	public static Vector3 mouseClickPoint;
	public Transform playerTransform;

	private float rayCastLength = 500;

	void Awake()
	{
		mouseClickPoint = Vector3.zero;
		NavalCameraMovement.target = GameObject.Find("Player").transform;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, rayCastLength)) {
			if (Input.GetMouseButton (0))
			{
				if (hit.collider.tag== "Enemy")
				{
					mouseClickPoint = hit.point;
					NavalCameraMovement.target = hit.collider.gameObject.transform;
				}
			}
		} 
		if (Input.GetMouseButton(2)) {
			NavalCameraMovement.target = GameObject.Find("Player").transform;
		}

		Debug.DrawRay (ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
	}
}
