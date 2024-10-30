using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {
	public GameObject xObject;
	public GameObject yObject;
	private bool clicked;
	private float heightInWorld;
	private float widthInWorld;
	private float time = 0.5f;
	//boardParams
	private float boardMinX;
	private float boardMaxX;
	private float boardMinY;
	private float boardMaxY;
	//restartButtonParams;
	//public static bool enabledd = true;

	//algorithm params
	private static int[,] board_array;
	public static bool player_turn;
	private static bool finished = false;
	private static int difficulty;
	private static bool XorO;// x is 0

	private static int[,] agent_board;
	private static bool agentsFirstMove;
	int max = 100;
	int min = -100;
	private List<Vector2> bestPoints = new List<Vector2>();
	private List<int> values = new List<int>();

	private static List<GameObject> removeObjects;

	public Text resultText;
	public Animator text_result_animator;
	Touch T;

	public static void Restart(){
		difficulty = MenuUIManagerScript.difficulty;
		player_turn = MenuUIManagerScript.turn;
		XorO = MenuUIManagerScript.isToggleX;
		finished = false;
		agentsFirstMove = !player_turn;
		foreach (var obj in removeObjects) {
			Destroy (obj);
		}
		removeObjects.Clear ();
		board_array = new int[3,3];
		agent_board = new int[3,3];
		//resultText.text = "";
	}
	// Use this for initialization
	void Start () {
        //setting restart params
        removeObjects = new List<GameObject>();
		// restart and initialize params 
		Restart();

		heightInWorld = Camera.main.orthographicSize * 2.0f;
		widthInWorld = heightInWorld / Screen.height * Screen.width;
		Debug.Log ("World height and width: " + heightInWorld + " " + widthInWorld);

		//setting board params;
		GameObject board = GameObject.FindGameObjectWithTag("board");
		SpriteRenderer board_sr = board.GetComponent<SpriteRenderer> ();

		//Debug.Log ("board width: " + );
		boardMinX = board.transform.position.x - board_sr.sprite.bounds.size.x / 2.0f;
		boardMaxX = board.transform.position.x + board_sr.sprite.bounds.size.x / 2.0f;
		boardMinY = board.transform.position.y - board_sr.sprite.bounds.size.y / 2.0f;
		boardMaxY = board.transform.position.y + board_sr.sprite.bounds.size.y / 2.0f;
		Debug.Log ("board params: " +boardMinX + " " + boardMaxX + " " + boardMinY + " " + boardMaxY );
	}
	


	void Update(){

		Vector3 TapPosition = Vector3.zero;


        //Handle desktop input
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL


		if(Input.GetMouseButtonDown(0)){
			TapPosition = Input.mousePosition;
		}
			
#endif

        //Handle mobile input
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8


		if(Input.touchCount > 0)
			T = Input.GetTouch(0);

		if(T.phase == TouchPhase.Began)
			TapPosition = T.position;
		else
			return; //Not required touch type
#endif

        //Generate ray from touch position
        Ray R = Camera.main.ScreenPointToRay (TapPosition);
        
        //Get hits in scene to detect card selection
        RaycastHit Hit;


		if (Physics.Raycast (R, out Hit, 10)) {

			if (Hit.collider.gameObject.CompareTag("board")) {
				if(player_turn && !finished){
					int y = (int)(Hit.point.x - boardMinX) / (int)((boardMaxX - boardMinX)/3.0f);
					int x = (int)(boardMaxY - Hit.point.y ) / (int)((boardMaxY - boardMinX)/3.0f);
					if(board_array[x,y] == 0){
						board_array[x,y] = 1;
						PlayerMove(x, y);
						player_turn = false;
					}


					for(int i = 0; i < 3; i++){
						for(int j = 0; j < 3; j++){
							Debug.LogWarning(board_array[i,j] + " ");
						}
						
					}
				}
			}
		}


	}

	void FixedUpdate(){
		if (!player_turn && !finished) {
			time -= Time.deltaTime;
			if(time <= 0){
				AgentMove();
				time = 0.5f;
			}
				
		}

		FinishGame ();
		if (finished) {
			if(HasPlayerWon(board_array))
				resultText.text = "You Win :)";
			else if(HasAgentWon(board_array))
				resultText.text = "You Lose :(";
			else
				resultText.text = "Draw";
		}
		text_result_animator.SetBool ("isHidden", !finished);
	}

	void PlayerMove(int x, int y){
		float centerX = (boardMaxX + boardMinX) / 2.0f;
		float centerY = (boardMaxY + boardMinY) / 2.0f;
		float pointX = (y - 1)* 3.0f + centerX;
		float pointY = (1 - x)* 3.0f + centerY;
		GameObject X;
		if (XorO) {
			X = (GameObject)Instantiate (xObject);
			X.transform.position = new Vector3 ((pointX), (pointY), -2.0f);
		} else {
			X = (GameObject)Instantiate(yObject);
			X.transform.position = new Vector3((pointX), (pointY), -2.0f);
		}

		removeObjects.Add (X);
	}

	void AgentMove(){
		int x, y;
		if (agentsFirstMove) {
			x = Random.Range (0, 3);
			y = Random.Range (0, 3);
			board_array[x,y] = 2;
			agentsFirstMove = false;
		} else {
			Vector2 pos = getBestPos ();
			x = (int) pos.x;
			y = (int) pos.y;
			board_array[(int)pos.x, (int)pos.y] = 2;
		}

		float centerX = (boardMaxX + boardMinX) / 2.0f;
		float centerY = (boardMaxY + boardMinY) / 2.0f;
		float pointX = (y - 1)* 3.0f + centerX;
		float pointY = (1 - x)* 3.0f + centerY;

		GameObject X;
		if (XorO) {
			X = (GameObject)Instantiate (yObject);
			X.transform.position = new Vector3 ((pointX), (pointY), -2.0f);
		} else {
			X = (GameObject)Instantiate (xObject);
			X.transform.position = new Vector3 ((pointX), (pointY), -2.0f);
		}


		removeObjects.Add(X);
		player_turn = true;

	}

	bool HasPlayerWon(int[,] board) {

		if ((board[0,0] == board[1,1] && board[0,0] == board[2,2] && board[0,0] == 1) || (board[0,2] == board[1,1] && board[0,2] == board[2,0] && board[0,2] == 1)) {
			
			return true;
		}
		for (int i = 0; i < 3; ++i) {
			if (((board[i,0] == board[i,1] && board[i,0] == board[i,2] && board[i,0] == 1)
			     || (board[0,i] == board[1,i] && board[0,i] == board[2,i] && board[0,i] == 1))) {
				
				return true;
			}
		}
		return false;
	}

	bool HasAgentWon(int[,] board) {
		
		if ((board[0,0] == board[1,1] && board[0,0] == board[2,2] && board[0,0] == 2) || (board[0,2] == board[1,1] && board[0,2] == board[2,0] && board[0,2] == 2)) {
			
			return true;
		}
		for (int i = 0; i < 3; ++i) {
			if (((board[i,0] == board[i,1] && board[i,0] == board[i,2] && board[i,0] == 2)
			     || (board[0,i] == board[1,i] && board[0,i] == board[2,i] && board[0,i] == 2))) {
				
				return true;
			}
		}
		return false;
	}

	bool BoardFull(int[,] board){
		for (int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++){
				if(board[i,j]==0)
					return false;
			}
		}
		return true;
	}

	void FinishGame(){
		if (HasPlayerWon (board_array) || HasAgentWon (board_array) || BoardFull (board_array)) {
			finished = true;
		}
	}

	void addAMove(Vector2 v, int t){
		agent_board[(int)v.x,(int)v.y] = t;
	}
	
	 List<Vector2> getPosiblePositions(){
		List<Vector2> list = new List<Vector2>();
		for(int i = 0; i<3; i++){
			for(int j = 0; j< 3; j++){
				if(agent_board[i,j] == 0){
					Vector2 p = new Vector2(i, j);
					list.Add(p);
				}
			}
		}
		return list;
	}
	
	private Vector2 getBestPos(){
		
		for(int i = 0; i< 3; i++){
			for(int j = 0; j < 3; j++){
				agent_board[i,j] = board_array[i,j] ;
			}
			
		}
		Vector2 p;
		alphaBeta(min, max, 0, 2);
		int m = -10;
		int pos = -1;
		for(int i = 0; i< values.Count; i++){
			if(values[i] > m){
				m = values[i];
				pos = i;
				
			}
		}
		/*int size = values.size();
        for(int i = 0; i< size; i++){
            if(values.get(i) < m){
                values.remove(i);
                
                bestPoints.remove(i);
            }
        }*/
		//values.trimToSize();
		//bestPoints.trimToSize();
		//size = bestPoints.size();
		//pos = new Random().nextInt(size);
		p = bestPoints[pos];
		values.Clear();
		bestPoints.Clear();
		
		return p;
	}

	private bool isGameOver() {
		
		return (HasPlayerWon(agent_board) || HasAgentWon(agent_board) || BoardFull(agent_board));
	}

	private int evaluate(){
		if(HasAgentWon(agent_board)){
			return 1;
		}else if(HasPlayerWon(agent_board)){
			return -1;         
		}else
			return 0;
	}

	private int alphaBeta(int alpha, int beta,int depth, int turn){
		if(alpha >=  beta){
			if(turn == 2){
				return max;
			}else 
				return min;
		}
		List<Vector2> availablePoints = getPosiblePositions();

		
		
		int maxval = min;
		int minval = max;
		
		if(isGameOver() || depth == difficulty){
			return evaluate();
		}
		
		
		for(int i = 0; i < availablePoints.Count; i++){
			int current;
			Vector2 point = availablePoints[i];
			if(turn == 2){
				addAMove(point, 2);
				current = alphaBeta(alpha, beta, depth + 1, 1);
				maxval = (int)Mathf.Max((float)maxval, (float)current); 

				
				alpha = (int)Mathf.Max((float)current, (float)alpha);
				//System.out.println("alpha: " + alpha + "crnt : " + current);
				if(depth == 0){
					bestPoints.Add(point);
					values.Add(current);
					Debug.Log(current);
				}
			}else{
				addAMove(point, 1);
				current = alphaBeta(alpha, beta, depth+1, 2); 
				minval = (int)Mathf.Min((float)minval, (float)current);
				
				
				beta = (int)Mathf.Min((float)current, (float)beta);
			}
			
			
			agent_board[(int)point.x, (int)point.y] = 0;
		}
		return turn == 2 ? maxval : minval;
	}

}
