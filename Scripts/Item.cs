using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPrice;
	public enum ItemType {Curacion,Comida,Limpieza,Equipo}
	public ItemType itemType;
	public int itemEffectNum;
	//constructor de los items con cada parametro
	public Item(string nombre,int id,string descrip , Texture2D icono, int precio, ItemType tipo,int valorDeEfecto)
	{
		itemName=nombre;
		itemID=id;
		itemDesc=descrip;
		itemIcon=icono;
		itemPrice=precio;
		itemType=tipo;
		itemEffectNum=valorDeEfecto;

	}
	//constructor de los items vacios;
	public Item()
	{

	}

}
