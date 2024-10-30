using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
	public Animator board;
	public Animator btn_restart;

	public void RestartGame(){
		MouseController.Restart();
	}

	public void GoBack()
	{
        Application.LoadLevel("menu");
    }

}
