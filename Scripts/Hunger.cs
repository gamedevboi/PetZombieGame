using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour {
	public Texture2D FoodbarFondo;
	public Texture2D FoodBartexture;
	public Texture2D FoodIcon;
	private float hungerBarlenght;
	public int hunger;
	public int MaxHunguer=500;
	public float timer;
	private int tiempoDeHunger;
	public bool isHungerDisplayed;
	public GUISkin skin;
	private GUIStyle numbersStyle;
	// Use this for initialization
	void Start () 
	{
//		if(PlayerPrefs.GetInt("hungerSave")== null)
//			hunger=MaxHunguer;		
//		else
			hunger=PlayerPrefs.GetInt("hungerSave");

		tiempoDeHunger = GetComponent<DañoPorTiempo>().tiempoEnPerderHambre;
		hungerBarlenght=Screen.width/2;

		//styles
		numbersStyle= skin.GetStyle("letras GUI");
		
		//fontsizes and shit
		numbersStyle.fontSize = Screen.height/23;
		numbersStyle.normal.textColor = Color.white;
		numbersStyle.fontStyle = FontStyle.Bold;
		numbersStyle.alignment= TextAnchor.UpperLeft;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hungerBarlenght=(Screen.width/2)*(hunger/(float)MaxHunguer);
		AdjustCurrentHunger (0);
	}

	void OnGUI()
	{
		if(isHungerDisplayed)
		{
			GUI.DrawTexture(new Rect(Screen.width*0.25f,Screen.height* 0.1f,Screen.width * 0.5f,Screen.height * 0.05f),FoodbarFondo);
			GUI.DrawTexture(new Rect(Screen.width*0.25f,Screen.height* 0.1f,hungerBarlenght,Screen.height * 0.05f),FoodBartexture);
			GUI.DrawTexture(new Rect(Screen.width*0.1f,Screen.height* 0.08f,Screen.width*0.15f,Screen.width*0.15f),FoodIcon);
			GUI.Label(new Rect(Screen.width*0.25f,Screen.height* 0.115f, Screen.width * 0.5f, Screen.height * 0.2f), hunger + "/" + MaxHunguer,numbersStyle);
		}
	}
	public void AdjustCurrentHunger(int loqueAjustar)
	{
		timer += Time.deltaTime;
		hunger += loqueAjustar;
		if (timer > tiempoDeHunger)
		{
			hunger-=1;
			timer=0;
		}
		if (hunger<0)
			hunger=0;

		if (hunger > MaxHunguer)
			hunger=MaxHunguer;
	}

}
