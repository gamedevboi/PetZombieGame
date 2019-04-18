using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SavingData : MonoBehaviour {
	private HealthBar healthCurrent;
	private Hunger hungerCurrent;
	private List<Item>  accessInvent;

	// Use this for initialization
	void Start () {
		healthCurrent = GameObject.Find ("zombie").GetComponent<HealthBar>();
		hungerCurrent = GameObject.Find ("zombie").GetComponent<Hunger>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Saving();
	}
	void Saving()//todos los parametros que vamos a guardar.
	{
		PlayerPrefs.SetInt("mesSave",System.DateTime.Now.Month);
		PlayerPrefs.SetInt("diaSave",System.DateTime.Now.Day);
		PlayerPrefs.SetInt("horaSave",System.DateTime.Now.Hour);
		PlayerPrefs.SetInt("minutoSave",System.DateTime.Now.Minute);
		PlayerPrefs.SetInt("segundoSave",System.DateTime.Now.Second);
		PlayerPrefs.SetInt ("healthSave",healthCurrent.curHealth);
		PlayerPrefs.SetInt ("hungerSave",hungerCurrent.hunger);

		accessInvent = GameObject.Find ("Inventario").GetComponent<Inventario>().inventario;

//		for(int i=0; i < 9; i++)
//		{
//			PlayerPrefs.SetInt("inventorySaveID"+i, accessInvent[i].itemID);
//			Debug.Log (PlayerPrefs.GetInt("inventorySaveID"+i));
//			Debug.Log("El indice:"+i);
//		}

	}
		

}
