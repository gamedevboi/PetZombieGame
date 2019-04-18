using UnityEngine;
using System.Collections;

public class leftArrow : MonoBehaviour {

	private DisplayedItem accesoDisplayed;
	//private ItemDatabase databaseAccess;
	public GUISkin skin;
	public bool isLoopItems = false;
	public AudioClip click;
	// Use this for initialization
	void Start () {
		accesoDisplayed = GameObject.Find("Icono de Objeto").GetComponent<DisplayedItem>();
		//databaseAccess = GameObject.Find ("Item Database").GetComponent<ItemDatabase>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width *0.1f,
		                       Screen.height * 0.77f,
		                       Screen.width * 0.1f,
		                       Screen.width * 0.1f),"", skin.GetStyle("nada")))
//		{
//			if (accesoDisplayed.currentDispID != 100)
//				accesoDisplayed.currentDispID--;
//
//		}
		{
			accesoDisplayed.currentDispID--;
			audio.PlayOneShot(click,0.5f);
		}


	}

}
