using UnityEngine;
using System.Collections;

//Todo lo que vaya a ir en cuanto empiece el juego a correr debe de ir en este script.
public class FirstScriptInGame : MonoBehaviour {

	public GameObject resetedMainMenu;//solo para llamar al boolean reseted que esta en main menu
	public GameObject zombiReseted; //

		// Use this for initialization
	void Start () {
		
		if(GameObject.Find("zombie")==null && PlayerPrefs.GetInt("reseted")==1)
		{
			Instantiate (zombiReseted, new Vector3(0,-2,0), Quaternion.identity).name= "zombie";//antes lo  escribi como zombi y no recuerdo por que.	
			PlayerPrefs.SetInt("reseted",0);
			Debug.Log (PlayerPrefs.GetInt("reseted"));

		}


	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
