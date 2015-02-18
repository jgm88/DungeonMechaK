using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnersMecha : MonoBehaviour {

	//TODO CAMBIAR ESTO POR ALGO MAS SIMPLE; 
	//TODO SCRIPTS EN CADA MECHA QUE TENGAN SU COMPONENTE PARA RECUPERAR MANA Y COMUNICARSE CON LA PUERTA

	public GameObject avivar;
	public Transform [] puntosSpawn;
	
	private IList<GameObject> listaMechas;
	public GameObject [] flamas;
	public GameObject prefab;
	
	public int TotalMechas;
	// Use this for initialization
	
	private IList <Transform> colocados;
	void Start () {
		
		colocados=new List<Transform>();
		listaMechas= new List<GameObject>();
		if(TotalMechas>puntosSpawn.Length || TotalMechas<=0)
			TotalMechas=3;
		int asignados=0, valor;
		
		while(colocados.Count<3)
		{
			Random.seed=System.DateTime.Now.Second;
			valor =(int) Random.Range(0,3);
			if(!colocados.Contains(puntosSpawn[valor]))
				colocados.Add(puntosSpawn[valor]);
		}
		InstanciarMecha();
	
	}
	
	
	private void InstanciarMecha()
	{
		int i=0;
		foreach(Transform t in colocados){
			listaMechas.Add ( GameObject.Instantiate(prefab,t.position,Quaternion.identity) as GameObject);
			listaMechas[i].GetComponentInChildren<permitirAvivar>().guitextAvivar=avivar;
			listaMechas[i].GetComponentInChildren<ControlLight>().flama=flamas[i];
			i++;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
