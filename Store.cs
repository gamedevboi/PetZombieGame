//Objetivo: 
//1. tiene que poner los objetos de la tienda y al darles doble click comprarlos si tienes el 
//dinero suficiente
//2. Cambiar las pestañas entre Comsumibles, Zombis o Backgrouds

using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	private MenuInferior menuInferior;
	private Vector3 disTrans;

	// Use this for initialization
	void Start () {
		menuInferior= GameObject.Find("MenuInferior").GetComponent<MenuInferior>();
		disTrans= gameObject.transform.position;
		disTrans = new Vector3(0.5f,0.5f, -8);
		gameObject.transform.position = disTrans;
		gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
//		GUI.depth=500;
//		//Abre el inventario cuando isPack = true
//		if(menuInferior.isShop)
//			AbrirTienda();
//			else
//			gameObject.SetActive(false);
	}

	public void AbrirTienda()
	{

	}

}
