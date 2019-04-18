using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GameObject ZombieDefault;
	public bool reseted;
	public GUISkin botonesSkin;
	private Rect botonRect;
	private int fontSize;
	public AudioClip sound;
	// Use this for initialization
	void Start () {

		//botonesSkin.GetStyle("BotonesStyle").fontSize = fontSize;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		botonRect= new Rect(Screen.width/2 - Screen.width* 0.3f, 
		                    Screen.height/2 + Screen.height*0.15f , 
		                    Screen.width * 0.6f , Screen.width/3);
		fontSize= Mathf.RoundToInt( Mathf.Abs ( botonRect.width/botonRect.height*10));
		botonesSkin.GetStyle("BotonesStyle").fontSize = fontSize;


		if(GUI.Button (botonRect,"Continue",botonesSkin.GetStyle("BotonesStyle")))
		{
			Application.LoadLevel("Main Scene");
			audio.PlayOneShot(sound,0.1f);
		}

		if(GUI.Button (new Rect(botonRect.x,botonRect.y-(Screen.height*0.2f),botonRect.width,botonRect.height),"New Game",botonesSkin.GetStyle("BotonesStyle")))
		{
			audio.PlayOneShot(sound,0.1f);
			PlayerPrefs.SetInt("healthSave",100);
			PlayerPrefs.SetInt("hungerSave",500);
			Application.LoadLevel("Main Scene");
			PlayerPrefs.SetInt("reseted",1);

		}


	}
}
