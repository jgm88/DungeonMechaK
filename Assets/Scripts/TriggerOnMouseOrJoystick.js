#pragma strict

public var mouseDownSignals : SignalSender;
public var mouseUpSignals : SignalSender;
public var boton : String;
public var mecha : GameObject;


private var state : boolean = false;
public var cadencia : float = 2f;
private var espera : boolean = false;
private var liberarPoder : boolean= true;


#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
private var joysticks : Joystick[];

function Start () {
	joysticks = FindObjectsOfType (Joystick) as Joystick[];	
	componente = mecha.GetComponent<lightlife>();
}
#endif


function Update () {
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
	if (state == false && joysticks[0].tapCount > 0) {
		mouseDownSignals.SendSignals (this);
		state = true;
	}
	else if (joysticks[0].tapCount <= 0) {
		mouseUpSignals.SendSignals (this);
		state = false;
	}	
#else	
	#if !UNITY_EDITOR && (UNITY_XBOX360 || UNITY_PS3)
		// On consoles use the right trigger to fire
		var fireAxis : float = Input.GetAxis("TriggerFire");
		if (state == false && fireAxis >= 0.2) {
			mouseDownSignals.SendSignals (this);
			state = true;
		}
		else if (state == true && fireAxis < 0.2) {
			mouseUpSignals.SendSignals (this);
			state = false;
		}
	#else
		if (state == false && Input.GetKeyDown(boton)) {
			COAtacar();	
		}
		
		else if (state == true && Input.GetKeyUp(boton)) {
			mouseUpSignals.SendSignals (this);
			state = false;
		}
	#endif
#endif


}
//coroutina para bloquear segun la cadencia
function COAtacar(){
	if(!espera){
		mouseDownSignals.SendSignals (this);
	//	mecha.SendMessage("lanzarllamas",espera,SendMessageOptions.DontRequireReceiver);
		espera = true;
		state = true;
		yield WaitForSeconds(cadencia);
		espera = false;
	}
}
