using UnityEngine;
using System.Collections;

public class NamePanelScript : MonoBehaviour {
	public Animator btn_start;
	public Animator btn_settings;
	public Animator btn_exit;
	public void SetButtonAnimation(){
		btn_start.enabled = true;
		btn_settings.enabled = true;
		btn_exit.enabled = true;
	}
}
