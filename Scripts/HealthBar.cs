using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public Texture2D healthBarFondo, ventanaFondo;
	public Texture2D healthBartexture;
	public Texture2D HealthIcon;
	public int maxHealth=100;
	public int curHealth;
	public bool isPetDead;
	public float timer;//distinto del de hunger
	private float healthBarlenght;
	private int tiempoDeHealth;
	public bool isHealthDisplayed;
	public GUISkin skin;
	private GUIStyle numbersStyle;
	// Use this for initialization
	void Start () 
	{

//		if(PlayerPrefs.GetInt("healthSave")== null)
//			curHealth=maxHealth;		
//		else
//		curHealth=PlayerPrefs.GetInt("healthSave");

		tiempoDeHealth = GetComponent<DañoPorTiempo>().tiempoEnPerderVida;
		healthBarlenght=Screen.width * 0.5f;
		isHealthDisplayed = true;
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
		AdjustcurHealth(0);
	}

	//barra de vida
	void OnGUI()
	{
		GUI.depth = 50;
		if(isHealthDisplayed)
		{
			GUI.DrawTexture (new Rect(Screen.width*0.0025f,Screen.height* 0.0025f,Screen.width,Screen.height * 0.18f),ventanaFondo);
			GUI.DrawTexture(new Rect(Screen.width*0.25f,Screen.height* 0.025f,Screen.width * 0.5f,Screen.height * 0.05f),healthBarFondo);
			GUI.DrawTexture(new Rect(Screen.width*0.25f,Screen.height* 0.025f,healthBarlenght,Screen.height * 0.05f),healthBartexture);
			GUI.DrawTexture(new Rect(Screen.width*0.1f,Screen.height* 0.01f,Screen.width*0.15f,Screen.width*0.15f),HealthIcon);
			GUI.Label(new Rect(Screen.width * 0.25f,Screen.height* 0.038f,Screen.width * 0.5f,Screen.height * 0.2f), curHealth + "/" + maxHealth,numbersStyle);
		}
	}
	//cuando es dañado o algo asi.
	public void AdjustcurHealth (int adj)
	{
		timer += Time.deltaTime;
		curHealth+=adj;//cambia la vida en la barra

		if (curHealth<0)//solo para que no exista vida negativa.
			curHealth=0;

		if(curHealth>maxHealth)//no puedes tener mas vida del maximo
			curHealth=maxHealth;

		if(maxHealth<1)//La maxima vida no puede ser cero porque si no se divide por cero en la sig linea.
			maxHealth=1;

		healthBarlenght=(Screen.width/2)*(curHealth/(float)maxHealth);

		//MUERTE-------------------------------------------------
		if(curHealth==0)
		{
			Destroy (this.gameObject);
			isPetDead=true;

		}
		//----------------------------------------------------------------
		//pierde vida cuando tiene hambre+++++++++++++++++++++++++++++++++
		if (timer > tiempoDeHealth)
		{
			timer=0;
			if (GetComponent<Hunger>().hunger == 0)
				curHealth-=1;
		}
		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	}

}
