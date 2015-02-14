using UnityEngine;
using System.Collections;

public class ligthLife : MonoBehaviour {
	
	
	/// <summary>
	/// Velocidad a la que se apaga la luz
	/// </summary>
	public float deadVelocity;
	
	/// <summary>
	/// compenentes de la antorcha para decrementar la fuerza de la luz
	/// </summary>
	private Torchelight torch;
	
	private GUITexture fire_bar;
	Color colorMain;
	private float totalBar, actualBar;
	
	/// <summary>
	/// Variable de incremento de luz
	/// </summary>
	public float lightMaker;
	/// <summary>
	/// Variable para controlar si se puede incrementar la intensidad de la luz.
	/// </summary>
	
	public bool comeToTurnOn;
	
	public float porcentajeFuego;
	public float porcentajeBomba;
	private bool enBoss=false;
	
	// Use this for initialization
	void Start () {
		
		if(deadVelocity<=0.0f || deadVelocity>=0.1)
			deadVelocity=0.03f;
		
		if(lightMaker<=0.0f|| lightMaker>=5.0f)
			lightMaker=0.023f;
		
		torch=gameObject.GetComponent<Torchelight>();
		fire_bar= GameObject.Find("FireBar").GetComponent<GUITexture>();
		colorMain=fire_bar.color;
		totalBar=fire_bar.pixelInset.height;
	
		
		if(porcentajeBomba<=0.0)
			porcentajeBomba=0.25f;
		if(porcentajeFuego<=0.3)
			porcentajeFuego=0.15f;
	}
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.F))
			turnOn();
		
	}
	public bool canBomba(){
		if(actualBar/totalBar>=porcentajeBomba)
			return true;
		return false;
	}
	public bool canFire(){
		if(actualBar/totalBar>=porcentajeFuego)
			return true;
		return false;
	}
	public void lanzarllamas()
	{
		if(actualBar/totalBar>=porcentajeFuego)
			actualizarBar(porcentajeFuego);	
	}
	
	public void lanzarBomba()
	{
		if(actualBar/totalBar>=porcentajeBomba)
			actualizarBar(porcentajeBomba);
	}
	
	void FixedUpdate()
	{
		if(enBoss)
			superTurnOn();
		else
			turnOff();
	}
	public void enBossOn(){
		enBoss=true;
	}
	private void actualizarBar()
	{
		
		actualBar=torch.IntensityLight*totalBar;
		moveBar();
		
		
	}
	private void moveBar()
	{
		if(actualBar>totalBar)
			actualBar=totalBar;
		
		if(actualBar/totalBar<=0.25)
			fire_bar.color=Color.red;
		else
			fire_bar.color=colorMain;
		
		
		fire_bar.pixelInset= new Rect(fire_bar.pixelInset.x,fire_bar.pixelInset.y,fire_bar.pixelInset.width,actualBar);
	}
	
	public void actualizarBar(float porcentaje)
	{
		actualBar-= totalBar*porcentaje;
		torch.IntensityLight=actualBar/totalBar;
		moveBar();
	}
	
	private void turnOff()
	{	
		
		torch.IntensityLight-=deadVelocity*Time.deltaTime;
		actualizarBar();
		
	}
	public void superTurnOn(){
		if(torch.IntensityLight<=1.0f)
				torch.IntensityLight+=lightMaker*Time.deltaTime*0.02f;
			
			actualizarBar();
	}
	
	public void turnOn()
	{
		if(comeToTurnOn)
		{
			if(torch.IntensityLight<=1.0f)
				torch.IntensityLight+=lightMaker*Time.deltaTime;
			
			actualizarBar();
		}
			
	}
	
}
