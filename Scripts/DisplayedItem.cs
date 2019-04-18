//THIS IS MOSTLE THE ENTIRE STORE SCRIPT
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayedItem : MonoBehaviour {

	public List<Item> itemDysplayed = new List<Item>();
	private ItemDatabase accessDatabase;
	public int currentDispID = 100;
	public GUISkin guiSkin;
	private GUIStyle guiStyle;
	private GUIStyle guiStyleDos, guiStyleTres, guiStyleBoton, guiStyleBotonDos;
	private bool accessIsLoop;
	private int mainMoney;
	private	bool isItASpace; //para revisar si hay un item vacio en el inventario.
	private Inventario accesoInventario;
	private bool abrirMensaje;
	public AudioClip cashSound, mesageSound,clickSound;

	// Use this for initialization
	void Start () {

		accessDatabase= GameObject.Find("Item Database").GetComponent<ItemDatabase>();
		itemDysplayed=accessDatabase.item; //asigno la lista con todos los objetos de la base de datos.
		guiStyle = guiSkin.GetStyle("solo letras");
		guiStyleDos = guiSkin.GetStyle("precio style");
		guiStyleTres = guiSkin.GetStyle("Mensajes");
		guiStyleBoton = guiSkin.GetStyle("BotonesStyle");
		guiStyleBotonDos = guiSkin.GetStyle("Button");
		accessIsLoop = GameObject.Find ("Arrow left").GetComponent<leftArrow>().isLoopItems;

		accesoInventario = GameObject.Find ("Inventario").GetComponent<Inventario>();
		abrirMensaje=false;		
	}
	
	// Update is called once per frame
	void Update () {

		if(currentDispID > 99 + accessDatabase.item.Capacity)
			currentDispID = 100;
		
		 if(currentDispID < 100)
			currentDispID = 99 + accessDatabase.item.Capacity;


		if(GameObject.Find ("Arrow left").GetComponent<leftArrow>().isLoopItems == true)
		{
			currentDispID = 100 + accessDatabase.item.Capacity;
			//Debug.Log ("en el bool:" + currentDispID);
			accessIsLoop=false;
			GameObject.Find ("Arrow left").GetComponent<leftArrow>().isLoopItems= false;

		}

		gameObject.guiTexture.texture= itemDysplayed[currentDispID - 100].itemIcon;

		guiStyle.fontSize = Screen.height/24;
		guiStyleDos.fontSize = Screen.height/25;
		guiStyleTres.fontSize = Screen.height/23;
		guiStyleBoton.fontSize = Screen.height/23;
		guiStyleBotonDos.fontSize = Screen.height/23;
		//para que se actualice el dinero en tiempo real
		mainMoney = GameObject.Find ("Game Manager").GetComponent<MoneyManager>().myCoins;


	}

	void OnGUI()
	{
		GUI.depth=500;

		//descripcion
		GUI.Box (new Rect(Screen.width*0.4f,
		                  Screen.height*0.45f,
		                  Screen.width * 0.48f,
		                  Screen.height*0.22f),
		         			itemDysplayed[currentDispID-100].itemDesc, guiStyle);

		//titulo
		GUI.Box (new Rect(Screen.width*0.4f,
		                  Screen.height*0.34f,
		                  Screen.width * 0.48f,
		                  Screen.height*0.22f),
		         itemDysplayed[currentDispID-100].itemName + ":", guiStyle);

		//precio
		GUI.Box (new Rect(Screen.width*0.015f,
		                  Screen.height*0.53f,
		                  Screen.width * 0.48f,
		                  Screen.height*0.22f),
		         "Price:"+ itemDysplayed[currentDispID-100].itemPrice,guiStyleDos);

		// Si Boton de comprar y dinero es mayoor que precio entonces:
		bool buyButton = GUI.Button (new Rect (Screen.width*0.20f,
		                                       Screen.height*0.65f,
		                                       Screen.width * 0.6f,
		                                       Screen.height*0.2f),"Buy",guiStyleBotonDos);

		//monedas que tienes
		GUI.TextArea(new Rect (Screen.width*0.1f,
		                        Screen.height*0.10f,
		                        Screen.width * 0.50f,
		                        Screen.height*0.25f),""+mainMoney,guiStyle);

		if ( buyButton ) 
		  {
			//es posible que quepa en el inventario? o hay un espacio vacio?
			for ( int i=0; i< accesoInventario.inventario.Count;i++)
			{
				if (accesoInventario.inventario[i].itemIcon == null)//si no tiene nombre, es vacio.
				{
					isItASpace=true;
					break;
				}else
					isItASpace=false;
			}

			if(isItASpace == true && mainMoney >= itemDysplayed[currentDispID-100].itemPrice)
			{
				accesoInventario.AgregarAInventario(currentDispID); //agregar el objeto que compramos al inventario.
				GameObject.Find ("Game Manager").GetComponent<MoneyManager>().myCoins -= itemDysplayed[currentDispID - 100].itemPrice; //tomar monedas

				audio.PlayOneShot(cashSound,0.5f);
			}else
			{
				abrirMensaje=true;//si no, mostrar ventana que dice que no tienes fondos suficientes o espacio en inventario.
				audio.PlayOneShot(mesageSound,0.5f);
			}


		
		
		}
		if(abrirMensaje == true)
		{
			GUI.Box (new Rect(Screen.width*0.03f,
			                  Screen.height*0.40f,
			                  Screen.width * 0.95f,
			                  Screen.height*0.30f),"You don't have enough Coins or your pack is full",guiStyleTres);
			
			bool isOK = GUI.Button (new Rect(Screen.width*0.30f,
			                                 Screen.height*0.55f,
			                                 Screen.width * 0.400f,
			                                 Screen.height*0.10f),"OK",guiStyleBoton);

			if (isOK ==true) 
			{
				audio.PlayOneShot(clickSound,0.5f);
				abrirMensaje=false;
			}
			
		}


	}

}
