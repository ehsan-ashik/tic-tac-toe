using UnityEngine;
using System.Collections;

public class SpriteScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Debug.Log ("Sprite widthHeight: " + Screen.width + " " + sr.sprite.bounds.size.x + " " + sr.sprite.bounds.size.y);
		float worldHeight = Camera.main.orthographicSize * 2.0f;
		float worldWidth = worldHeight / Screen.height * Screen.width;
		//float ratio = worldWidth / worldHeight;
		transform.localScale = new Vector3 (worldWidth/sr.sprite.bounds.size.x, worldHeight/sr.sprite.bounds.size.y, 1);
		//Debug.Log ("Sprite widthHeight: " + Screen.width + " " + sr.sprite.texture.width + " " + sr.sprite.texture.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
