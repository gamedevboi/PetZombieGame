 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventario : MonoBehaviour {
	public List<Item> inventario = new List<Item>();
	public List<Item> espacios = new List<Item>();
	private ItemDatabase database;
	public AudioClip click;//AUDIOS
	[SerializeField]
	public GUISkin skinGUI;
	private GUIStyle styleDescripcion, styleCruz;
	public Texture2D slotIconTexture;
	public Item itemVacio = new Item();
	public int espaciosX=3,espaciosY=3;
	public bool descripcionActiva;
	public Item itemTransicion; //para guardar info de un item en la interfaz mientras lo mueves o cosas asi,
	public bool arrastrando;
	public MenuInferior menuInferior;
	public Vector2 recuadrosMenuSize;//tamaño de los recuadros que conforman los espacios del inventario.
	private int j;//cuenta indices secundarios si son necesarios solamente.



	// Use this for initialization
	void Start () 
	{
		//El script esta en la camara principal por eso tengo que buscarlo, quiero llamar al bool isPack desde aqui.
		menuInferior= GameObject.Find("MenuInferior").GetComponent<MenuInferior>();
		//añade items vacios al inventario y a los espacios.
		for (int i =0; i < espaciosX*espaciosY;i++)
		{
			espacios.Add(itemVacio);
			inventario.Add (itemVacio);
		}
		//llamo a las componentes de ItemDatabase para poder usarlas para crear items y asi.
		database= GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		//NOTA: Manera de agregar objetos sin la funcion: inventario.Add (database.item[0]);
		//NOTA: AgregarAInventario (105);
		//NOTA: EliminarDelInventario(100);
		AgregarAInventario (100);
		AgregarAInventario (101);
		AgregarAInventario (102);
		AgregarAInventario (103);
		AgregarAInventario (104);
		AgregarAInventario (105);
		AgregarAInventario (100);
		AgregarAInventario (101);
		AgregarAInventario (102);
		//Estilos:
		styleDescripcion= skinGUI.GetStyle("Descripcion");
		styleCruz = skinGUI.GetStyle("BotonExit");

		//Tamaño Fuentes:
		styleDescripcion.fontSize = Screen.height/23;
	}


	void Update ()
	{

	}
	void OnGUI()
	{
		GUI.depth=100;
		//Abre el inventario cuando isPack = true
		if(menuInferior.isPack)
			AbrirInventario();



	}

	public void AbrirInventario()
	{	
		Event e= Event.current;//para escribir menos nomas
		//Dibuja los iconos y la bolsa de items---------------------------------------------------------------	
		int i=0;//lleva un conteo de cada item
		
		for (int y=0;y<espaciosY;y++)
		{
			for(int x=0;x<espaciosX;x++)
			{
				//Operaciones con recInventario para asegurar que se conserve la proporcion siempre sin importar el tamaño de la pantalla**************************************************************************
				recuadrosMenuSize = new Vector2(Screen.width*0.15f,Screen.width*0.15f);//no importa la pantalla la proporcion sera la misma siempre
//				Rect recInventario = new Rect((Camera.main.WorldToScreenPoint(menuInferior.transform.position).x  + recuadrosMenuSize.x )*x,
//				                              ((Screen.height-recuadrosMenuSize.y *y)-menuInferior.tamanoBotones.y) - Camera.main.WorldToScreenPoint(menuInferior.transform.position).y/2,
//				                            recuadrosMenuSize.x,recuadrosMenuSize.y);	
				//recuadrosMenuSize = new Vector2(32,32*ratio);
				Rect recInventario = new Rect (recuadrosMenuSize.x*x + menuInferior.tamanoBotones.x,
				                               -recuadrosMenuSize.y*y + Screen.height - menuInferior.tamanoBotones.y,
				                               recuadrosMenuSize.x, 
				                               recuadrosMenuSize.y);
				//Rect recPrueba = new Rect (Screen.width/2,Screen.height/2,32,32);

				//***************************************************************************************************************************************************************************************
				//GUI.Box(recInventario,"",skinGUI.GetStyle("Inventario"));
				GUI.DrawTexture(recInventario, slotIconTexture);
				espacios[i]=inventario[i];

				//si el objeto del inventario no tiene icono es porque no esta en ninguna bolsa
				//entonces sirve esto para dibujar los objetos que si tenemos o para interactuar
				//con lo que si tenemos
				if(inventario[i].itemIcon!=null)	
				{
					GUI.DrawTexture(recInventario,espacios[i].itemIcon);	
					
					//por el momento es la interfaz con el mouse.	
					if(recInventario.Contains (e.mousePosition)&& Input.GetMouseButton(0)&& e.clickCount==2 )//&& EventType.MouseDrag==Event.current.type )
					{//si haces doble click sobre un icono pasa lo de abajo
						audio.PlayOneShot(click,0.5f);
						descripcionActiva= true;//activa la descripcion de los objetos
						itemTransicion= inventario[i];
					}
					
					//cuadro de descripcion
					if(descripcionActiva)
					{
						string descripcion= "  " + itemTransicion.itemName + "\n   " + 
							itemTransicion.itemDesc + "\n\nPrice:"+
								"  " + itemTransicion.itemPrice;
						
						GUI.Box (new Rect(Screen.width/8,Screen.height/4,6*Screen.width/8,4*Screen.width/8),descripcion,styleDescripcion);
						
						if(GUI.Button(new Rect(Screen.width/8 + 6*Screen.width/8 - Screen.width/10, Screen.height/4, Screen.width/10,Screen.width/10),"",styleCruz))
						{
							audio.PlayOneShot(click,0.5f);
							descripcionActiva= false;
						}
						
					}

					//*******************************************arrastrar Items:******************************************************************
					//si estoy presionando el boton derecho del mouse y lo arrastro, y estoy sobre un item y no estoy arrastrando nada ya, entonces:
					if(Event.current.button == 0 && Event.current.type == EventType.mouseDrag && recInventario.Contains(e.mousePosition) && !arrastrando)
					{
						arrastrando= true;
						descripcionActiva= false;
						itemTransicion=espacios[i];
						j= i;
						inventario[i]= itemVacio;
						
					}
					//cuando estas arrastrando items:
					if (arrastrando)
					{
						Rect mouseRec = new Rect(e.mousePosition.x,e.mousePosition.y, Screen.width/7,Screen.width/7);	
						GUI.DrawTexture(mouseRec,itemTransicion.itemIcon);
					}
					//si estas arrastrando algo, estas sobre un item y levantas el click entonces:
					if (arrastrando && e.type==EventType.mouseUp && recInventario.Contains(e.mousePosition))
					{
						inventario[j]=inventario[i];	
						inventario[i]=itemTransicion;
						arrastrando=false;
						itemTransicion=null;
					}
					
				}else//Todo lo de que es que para slots que no contienen items
				{
					//si no contiene item y lo arrastas a un espacio => ahi lo va a poner.
					if(arrastrando && e.type==EventType.mouseUp && recInventario.Contains(e.mousePosition))
					{
						inventario[i]=itemTransicion;
						arrastrando=false;
						itemTransicion=null;
					}
				}
				//InteractionWithInventory simon= InteractionWithInventory.InteraccionConInventario(i,recInventario,e);
				//variable que lleva la cuenta de que objeto es.
				i++;		
			}
		}
	}

	//********estos dos los hice con el fin de agregar y quitar objetos usando la ID****************************
	public void AgregarAInventario(int loQueAgregarID)
	{
		Item loQueAgregar=itemVacio;
		for(int j=0;j<database.item.Count;j++)
		{
			if(loQueAgregarID==database.item[j].itemID)
			{
				loQueAgregar=database.item[j];
				break;
			}
		}


		for(int i=0; i < inventario.Count;i++)
		{
			if(inventario[i].itemID==0)
			{
				inventario[i]=loQueAgregar;
				break;
			}
		}

	}

	public void EliminarDelInventario(int loQueEliminarID)//elimina todo lo que tenga la ID que le pongas, del inventario.
	{

		for(int i=0; i< inventario.Count; i++)
		{
			if(inventario[i].itemID==loQueEliminarID)
			{
				inventario[i]=itemVacio;
				break;
			}

		}
		return;
	}
	//Encuentra un Item en el inventario
	public Item BuscadoEnInventario(int loQueBuscoID)
	{
		Item unItem=itemVacio;
		for(int i=0; i< inventario.Count;i++)
		{
			if(inventario[i].itemID==loQueBuscoID)
			{
				inventario[i]=unItem;
				break;
			}
		}
		return(unItem); //no estoy seguro si es unItem o solo return. No he usado esta funcion aun.
	}
	//*****************************************************************************************************
	





}//End of Class

