using UnityEngine;
using System.Collections;

public class MiniMapCameraBehaviour : MonoBehaviour {

	public Transform target;
	private Vector3 _vPosition;

	void Start () {
		target = GameObject.Find("Player").transform;
	}

	void LateUpdate () {
		_vPosition = target.transform.position;
		_vPosition.y = 20;
		transform.position = _vPosition;
	}
}
