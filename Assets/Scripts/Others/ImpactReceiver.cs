using UnityEngine;

public class ImpactReceiver: MonoBehaviour {
  
	public float mass = 3.0f; // define the character mass
	
	Vector3 impact = Vector3.zero;
	CharacterController character;
	Vector3 center;
	
	void Start(){
	character = GetComponent< CharacterController>();
	}
	
	public void AddImpact(Vector3 force)
	{ // CharacterController version of AddForce
		impact += force / mass;
	}
	
	void Update(){ 
		
//		if(gameObject.tag=="Enemy")
//			character.center=new Vector3(character.center.x,0f,character.center.z);
//		
		// apply the impact effect:
		if (impact.magnitude > 0.2f)
		{
		  character.Move(impact * Time.deltaTime);
		}
		// impact energy goes by over time:
		impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
	}
}
