using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	public GUISkin skin;
	public AudioClip sound;
	void OnGUI()
	{
		if(GUI.Button(new Rect( Screen.width *0.85f ,Screen.height * 0.005f,Screen.width*0.11f,Screen.width*0.11f),"",skin.GetStyle("BotonExit")))
		{
			Destroy(GameObject.Find ("zombie"));
			audio.PlayOneShot(sound,1);
			Application.LoadLevel ("MainMenu");
		}
	}
}
