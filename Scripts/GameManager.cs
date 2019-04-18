using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private BoxCollider2D zombiCollider2D;
	public Rect zombiRect;
	public bool isUsingItem;
	private Inventario accesoAInventario;
	private HealthBar vida;
	private Hunger referenciaHambre;
	public LayerMask zombiLayer;
	private Item loQueEliminar;
	public AudioClip useItem;
	void Start()
	{
		vida= GameObject.Find("zombie").GetComponent<HealthBar>();
		referenciaHambre= GameObject.Find("zombie").GetComponent<Hunger>();
		accesoAInventario= GameObject.FindGameObjectWithTag("Inventario").GetComponent<Inventario>();
	}
	void Update()
	{
		LinecastsAndShit();//tracks the item and use it when its over the zombie and shit YOLO SWAG
	}
	//it uses items and shit dawg.
	public void UsoDeItem(Item itemPorUsar)//el int es lo que se le sube o baja a cierta propiedad;
	{
		audio.PlayOneShot(useItem,0.5f);
		//las tres lineas siguientes son para hacer que se elimine el item del inventario correctamente,.
		accesoAInventario.arrastrando=false;
		accesoAInventario.EliminarDelInventario (accesoAInventario.itemTransicion.itemID);
		accesoAInventario.itemTransicion= null;

		//checks what kind of item it is then does the effect.
		if (itemPorUsar.itemType==Item.ItemType.Curacion)
		{
			vida.AdjustcurHealth(itemPorUsar.itemEffectNum);
		}
		if (itemPorUsar.itemType== Item.ItemType.Comida)
		{
			referenciaHambre.AdjustCurrentHunger(itemPorUsar.itemEffectNum); 
		}


	}

	public void LinecastsAndShit()//hace tres linecasts para seguir la posicion del mouse cuando estas arrastrando un item Y si lo sueltas en el zombi => lo usa.
	{
		if (accesoAInventario.arrastrando)
		{
			Ray rayo= Camera.main.ScreenPointToRay(Input.mousePosition);//SIRVE PARA ENCONTRAR LA POSICION FINAL DEL RAYO (TO TRACK THE MOUSE MOUSE).
			Vector2 posFinal= new Vector2(rayo.origin.x,rayo.origin.y);
			Vector2 origenRayo2= GameObject.Find("OrigenRayo2").GetComponent<Transform>().position;
			Vector2 origenRayo3= GameObject.Find("OrigenRayo3").GetComponent<Transform>().position;

			bool isOverTheZombi1= Physics2D.Linecast (this.transform.position, posFinal, zombiLayer);//linecast para ver si esta arriba del zombi.
			bool isOverTheZombi2= Physics2D.Linecast (origenRayo2, posFinal, zombiLayer);
			bool isOverTheZombi3= Physics2D.Linecast (origenRayo3 ,posFinal, zombiLayer);

			Debug.DrawLine(this.transform.position,posFinal);//DEBUG PURPOSES
			Debug.DrawLine(origenRayo2,posFinal,Color.red);
			Debug.DrawLine(origenRayo3,posFinal,Color.green);

			//si la triangulacion esta sobre el zombi y ademas se levanta el boton del mouse, se usa el item.
			if(isOverTheZombi1 && isOverTheZombi2 &&isOverTheZombi3 && Input.GetMouseButtonUp(0))
			{
				UsoDeItem(accesoAInventario.itemTransicion);
			}
			
		}
	}
}
