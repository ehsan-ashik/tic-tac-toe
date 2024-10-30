using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIManagerScript : MonoBehaviour {
	public Toggle youToggle;
	public Toggle agentToogle;
	public static bool turn;

	public Animator btn_start;
	public Animator btn_settings;
	public Animator btn_exit;
	public Animator pnl_chose;

	//settins play with Button;
	public Toggle toggleX;
	public Toggle toggleO;
	public static bool isToggleX = true;

	//settings difficulty;
	public Toggle toggleNormal;
	public Toggle toggleHard;
	public Toggle toggleUnbt;
	public static int difficulty = 10;
	public Animator pnl_settings;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ImplementToggleGroupYou(){
		agentToogle.isOn = !youToggle.isOn;
	}

	public void ImplementToggleGroupAgent(){
		youToggle.isOn = !agentToogle.isOn;
	}

	public void ImplementToggleGroupO(){
		toggleX.isOn = !toggleO.isOn;
		isToggleX = toggleX.isOn;
	}
	public void ImplementToggleGroupX(){
		toggleO.isOn = !toggleX.isOn;
		isToggleX = toggleX.isOn;
	}

	public void ImplementToggleGroupNormal(){
		Debug.Log ("diffi: " + difficulty);
		if (toggleNormal.isOn) {
			difficulty = 1;
			toggleHard.isOn = false;
			toggleUnbt.isOn = false;
		}
		if (!toggleNormal.isOn && !toggleHard.isOn && !toggleUnbt.isOn)
			toggleNormal.isOn = true;

	}

	public void ImplementToggleGroupHard(){
		Debug.Log ("diffi: " + difficulty);
		if (toggleHard.isOn) {
			difficulty = 3;
			toggleNormal.isOn = false;
			toggleUnbt.isOn = false;
		}
		if (!toggleNormal.isOn && !toggleHard.isOn && !toggleUnbt.isOn)
			toggleHard.isOn = true;
	}

	public void ImplementToggleGroupUnbt(){
		Debug.Log ("diffi: " + difficulty);
		if (toggleUnbt.isOn) {
			difficulty = 10;
			toggleHard.isOn = false;
			toggleNormal.isOn = false;
		}
		if (!toggleNormal.isOn && !toggleHard.isOn && !toggleUnbt.isOn)
			toggleUnbt.isOn = true;
	}

	public void UpdateDifficultyToggle()
	{
		if (difficulty == 1) {
			toggleNormal.isOn = true;
			toggleHard.isOn = toggleUnbt.isOn = false;
		} else if (difficulty == 3) {
            toggleHard.isOn = true;
            toggleNormal.isOn = toggleUnbt.isOn = false;
        } else {
            toggleUnbt.isOn = true;
            toggleHard.isOn = toggleNormal.isOn = false;
        }
	}

	public void UpdatePlayWithToggle()
	{

		toggleX.isOn = isToggleX;
		toggleO.isOn = !isToggleX;
	}

	public void Play(){
		if (youToggle.isOn) {
			MouseController.player_turn = true;
			turn = true;
		} else {
			MouseController.player_turn = false;
			turn = false;
		}

		Application.LoadLevel ("game");
	}

	public void OpenPanelChoose(){
		pnl_chose.enabled = true;
		pnl_chose.SetBool ("isHidden", false);
		btn_start.SetBool ("isHidden", true);
		btn_settings.SetBool ("isHidden", true);
		btn_exit.SetBool ("isHidden", true);
	}

	public void ClosePanelChoose(){
		btn_start.SetBool ("isHidden", false);
		btn_settings.SetBool ("isHidden", false);
		btn_exit.SetBool ("isHidden", false);
		pnl_chose.SetBool ("isHidden", true);
	}

	public void OpenPanelSettings(){
		pnl_settings.enabled = true;
		pnl_settings.SetBool ("isHidden", false);
		btn_start.SetBool ("isHidden", true);
		btn_settings.SetBool ("isHidden", true);
		btn_exit.SetBool ("isHidden", true);
		UpdateDifficultyToggle();
		UpdatePlayWithToggle();
	}

	public void ClosePanelSettings(){
		btn_start.SetBool ("isHidden", false);
		btn_settings.SetBool ("isHidden", false);
		btn_exit.SetBool ("isHidden", false);
		pnl_settings.SetBool ("isHidden", true);
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
