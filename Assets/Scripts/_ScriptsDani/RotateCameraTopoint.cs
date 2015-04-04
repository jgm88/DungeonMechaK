using UnityEngine;
using System.Collections;

public class RotateCameraTopoint : MonoBehaviour
{

//	private Transform camera;
	// Use this for initialization
	void Start ()
	{
//		camera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") {
			other.transform.rotation = Quaternion.Slerp (other.transform.rotation, this.transform.rotation, 3f * Time.deltaTime);
		}
	}
}
