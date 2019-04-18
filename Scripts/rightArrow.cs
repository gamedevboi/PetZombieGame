using UnityEngine;
using System.Collections;

public class rightArrow : MonoBehaviour {

	private DisplayedItem accesoDisplayed;
	public GUISkin skin;
	public AudioClip click;
	// Use this for initialization
	void Start () {
		accesoDisplayed = GameObject.Find("Icono de Objeto").GetComponent<DisplayedItem>();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width *0.8f,
		                       Screen.height * 0.77f,
		                       Screen.width * 0.1f,
		                       Screen.width * 0.1f),"", skin.GetStyle("nada")))
		{
			accesoDisplayed.currentDispID++;
			audio.PlayOneShot(click,0.5f);
		}
	}
}
