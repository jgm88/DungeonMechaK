using UnityEngine;
using System.Collections;

public class LoadAnimation : MonoBehaviour {
	
	OTAnimatingSprite animacion;
	
	/// <summary>
	/// The orientacion del pj
	/// 0-arriba
	/// 1-abajo
	/// 2-derecha
	/// 3-izquierda
	/// </summary>
	private static int orientacion;
	
	public float delay,atkTime;
	
	// Use this for initialization
	void Start () {
		animacion= this.GetComponent<OTAnimatingSprite>();
		orientacion=0;
		atkTime=0.0f;
	}
	
	private void andar()
	{
		
		if(!animacion.isPlaying)
		{
			if(Manejador.arriba )
			{
				animacion.Play("walk-arriba");
				orientacion=0;
				
			}
			else if(Manejador.abajo)
			{
				animacion.Play("walk-abajo");
				orientacion=1;
			}
			else if(Manejador.derecha)
			{
				animacion.Play("walk-der");
				orientacion=2;
				
			}
			else if(Manejador.izquierda)
			{
				animacion.Play("walk-izq");
				orientacion=3;
			}
		}
		
			
	}
	
	private void atacar()
	{
		
		if(!animacion.isPlaying)
		{
			
			if(Manejador.atack &&  orientacion==0)
			{
				animacion.Play("at-arriba");
			}
			else if(Manejador.atack && orientacion==1)
			{
				animacion.Play("at-abajo");
			}
			else if(Manejador.atack && orientacion==2)
			{
				animacion.Play("at-der");
			}
			else if(Manejador.atack && orientacion==3)
			{
				animacion.Play("at-izq");
			}
		}
			
	}
	
	private void stopFrame()
	{
		/*if(orientacion==0)
			animacion*/
	}
	
	// Update is called once per frame
	void Update () {
		andar();
		if(Manejador.atack)
			animacion.Stop();
		atacar();
		if(!Input.anyKeyDown)
			stopFrame();
	}
}
