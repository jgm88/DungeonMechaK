using UnityEngine;
using System.Collections;

public class MarkerBehaviour : MonoBehaviour {

	private Transform _tPlayer;
	private Vector3 _vRotation;
	// Use this for initialization
	void Start () {
		_tPlayer = GameObject.FindWithTag("Player").transform;
		_vRotation = new Vector3(180f,0f,0);
	}
	
	// Update is called once per frame
	void LateUpdate () {

		transform.position = _tPlayer.position;
		_vRotation.y = _tPlayer.eulerAngles.y;
		transform.eulerAngles = _vRotation;
//		transform.eulerAngles
	}

}
