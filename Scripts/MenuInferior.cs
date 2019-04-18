using UnityEngine;
using System.Collections;

public class MenuInferior : MonoBehaviour {
	public GUISkin skinMenuInferior;
	private bool botonPack;
	private bool botonShop;
	public Texture2D packIcon;
	public Texture2D shopIcon;
	public Vector2 tamanoBotones;
	public bool isPack;
	public bool isShop;
	private Inventario referenciaInventario;//Bugfixed: para no cerrar el inventario si estoy arrastrando algo.
	public AudioClip sound;
	public GameObject storeObject,cruzSalir, zombieObject;

	// Use this for initialization
	void Start () {
		isPack=false;
		referenciaInventario= GameObject.FindGameObjectWithTag("Inventario").GetComponent<Inventario>();
		tamanoBotones= new Vector2(Screen.width*0.2f,Screen.width*0.2f);
		cruzSalir= GameObject.Find ("exit button");//referencia para la cruz de exit game
		zombieObject = GameObject.Find ("zombie");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isShop)
		{
			storeObject.SetActive(true);
			zombieObject.GetComponent<HealthBar>().isHealthDisplayed=false;
			zombieObject.GetComponent<Hunger>().isHungerDisplayed=false;

			cruzSalir.SetActive(false);//desactiva la cruz para salir (exit game button)
		}

		else
		{
			storeObject.SetActive(false);
			zombieObject.GetComponent<HealthBar>().isHealthDisplayed=true;
			zombieObject.GetComponent<Hunger>().isHungerDisplayed=true;
			cruzSalir.SetActive (true);//cruzSalir.SetActive(true); //activa el boton exit game
		}

		}

	void OnGUI()
	{
		//Abre y cierra el inventario o mochila.
		if(botonPack && !referenciaInventario.arrastrando)
		{
			isPack = !isPack;
		audio.PlayOneShot(sound,0.05f);
		}
		//Abre y cierra la tienda
		if(botonShop && !referenciaInventario.arrastrando)
		{
			isShop = !isShop;
			audio.PlayOneShot(sound,0.05f);
		}

		GUI.depth=150;
		//Boton de backpack

		botonPack=GUI.Button(new Rect(Screen.width*0.0025f,//Camera.main.WorldToScreenPoint(this.transform.position).x,
		                              Screen.height- tamanoBotones.y,
		                              tamanoBotones.x,tamanoBotones.y),"",
			                     skinMenuInferior.FindStyle("MenuInferiorStyle") );

		botonShop=GUI.Button(new Rect(Screen.width- tamanoBotones.x, Screen.height- tamanoBotones.y,
			                              tamanoBotones.x,tamanoBotones.y),
			                     "",skinMenuInferior.FindStyle("BotonShop") );

	}
}
