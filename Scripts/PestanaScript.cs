using UnityEngine;
using System.Collections;

public class PestanaScript : MonoBehaviour {
	public GameObject objectsWinStore, zombieWinStore, backgWinStore;
	private int toolBarInt = 0;
	private string[] toolbarStrings = {"Items", "Zombies", "Backgrounds"};
	public GUISkin skinGUI;
	private GUIStyle styleBar;

	// Use this for initialization
	void Start () {
		styleBar = skinGUI.GetStyle("toolbar custom");

		objectsWinStore= GameObject.Find("Store Window Objects");//store the game object into this variable

	
	}
	
	// Update is called once per frame
	void Update () {
		styleBar.fontSize = Screen.height/40;
	}

	void OnGUI()
	{
		toolBarInt = GUI.Toolbar(new Rect(Screen.width * 0.06f,
		                                  Screen.height * 0.28f,
		                                  Screen.width * 0.878f ,
		                                  Screen.height * 0.12f),toolBarInt,toolbarStrings,styleBar);

		//ventana de items u objetos;
		if(toolBarInt == 0 )
		{
			objectsWinStore.SetActive(true);
		}else
		{
			objectsWinStore.SetActive(false);
		}
	}
}
