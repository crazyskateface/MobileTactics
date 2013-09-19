using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	bool selected = false;
	bool selectedToAct = false;
	//life
	//mana
	//damage
	
	
	//MOVEMENT
	public int moveableSpaces = 4;
	public int life;
	public int damage;
	
	//public GameObject selected_particles;
	
	int castableSpaces = 1;
	public Transform player;
	int distance = 1;
	Vector3 playerpos;
	Vector3 movDirection;
	bool isMoving = false;
	public bool isMovingToTile = false;
	Vector3 dest;
	int dir;
	GameObject[] tiles;
	GameObject tile;
	//List<int> list = new List<int>();
	List<Vector2> tilePositions;
	List<Vector2> solutionPath;
	Dictionary<Vector2, Color> tiles_dict;
	
	
	// Use this for initialization
	void Start () {
		player = transform;
		tiles = GameObject.FindGameObjectsWithTag("tile");
		solutionPath = new List<Vector2>();
		
		//produce the dict
		tiles_dict = new Dictionary<Vector2, Color>();
		
		//tilePositions = new Vector2[tiles.Length];
		tilePositions = new List<Vector2>();
		int i=0;
		//put the positions of each tile into an array of vector2's   [ (1,0), (2,2), Vector2(2,3) ]
		foreach(GameObject tile in tiles){
			Vector2 position = new Vector2(tile.transform.position.x,tile.transform.position.z);
			//tilePositions[i] = position;
			tilePositions.Add(position);
			//Debug.Log (position);
		}
	}
	
	// Update is called once per frame
	void Update () {		
		
		if(Input.GetKeyDown("d")){
			//fight	
			punch(0);
		}
		
//		if(selected_particles.renderer.enabled){
//			if(!selected){
//				selected_particles.renderer.enabled = false;		
//			}
//		}
//		else{
//			if(selected){
//				selected_particles.renderer.enabled = true;	
//			}
//		}
		
		
		
	}
	
	
	IEnumerator AI_move(Vector3 tile_dest){
		if(isMovingToTile == false){
			isMovingToTile = true;
			//transform.animation.Play ("walk");
			Vector3 org = player.position;
			dest = tile_dest;
			dest.y = org.y; //keep the y the same so it doesn't go underground bro ya kno
			Vector3 zdest = dest;
			zdest.x = org.x;
			Vector3 xdest = dest;
			//keep xdest same until later
			float t=0;
			float duration = 1;
			bool zmovement = true;
			//check if it needs to rotate now and if there is no zmovement
			if(zdest.z == org.z){
				zmovement = false;
				int xdiff = (int)dest.x - (int)org.x;
				
				if((int)player.forward.z == 1){//facing right
					if(xdiff <0){//going up?
						rotateCharacterLeft ();
					}else{ //going down?
						rotateCharacterRight ();
					}	
				}else if((int)player.forward.x == 1){ //facing down
					if(xdiff <0){//going up?
						rotateCharacterRight ();
					}else{ //going down?
						//rotateCharacterLeft ();
					}
				}else if((int)player.forward.x == -1){//facing up
					if(xdiff <0){
						// going up
					}else{//going down?
						rotateCharacterLeft (180);
					}
				}else{ //facing left
					if(xdiff<0){//going up?
						rotateCharacterRight ();
					}else{//down?
						rotateCharacterLeft ();
					}
				}
			}else{//there is zmovement ...am i facing the right way?
				int zdiff = (int)dest.z - (int)org.z;
				if((int)player.forward.x == 1){
					//facing down
					if(zdiff <0) //going left?
						rotateCharacterRight ();
					else
						rotateCharacterLeft ();
				}else if((int)player.forward.x == -1){//facing up
					if(zdiff <0) //going left?
						rotateCharacterLeft ();
					else
						rotateCharacterRight ();
				}else if((int)player.forward.z == -1){// facing left
					if(zdiff >0) //going right?
						rotateCharacterLeft(180);//turn 180
					
				}
				
			}
			//move character in z direction first
			if(zmovement){
				while(t < 1){
					t += Time.deltaTime /duration;
					player.position = Vector3.Lerp (org, zdest, t);
					yield return 0;
				}
			}
			org = player.position;
			xdest.z = org.z;
			
			if(zdest.z == org.z){
				zmovement = false;
				int xdiff = (int)dest.x - (int)org.x;
				if((int)player.forward.z == 1){//facing right
					if(xdiff <0){//going up?
						rotateCharacterLeft ();
					}else{ //going down?
						rotateCharacterRight ();
					}	
				}else if((int)player.forward.x == 1){ //facing down
					if(xdiff <0){//going up?
						rotateCharacterRight ();
					}else{ //going down?
						//rotateCharacterLeft ();
					}
				}else if((int)player.forward.x == -1){//facing up
					if(xdiff <0){
						// going up
					}else{//going down?
						rotateCharacterLeft (180);
					}
				}else{ //facing left
					if(xdiff<0){//going up?
						rotateCharacterRight ();
					}else{//down?
						rotateCharacterLeft ();
					}
				}
			}
			// put turning stuff here
			t=0;
			while(t < 1){
				//Debug.Log ("LSDJFKSJDLFJSDLFSD");
				t += Time.deltaTime /duration;
				player.position = Vector3.Lerp (org, xdest, t);
				yield return 0;
			}
			isMovingToTile = false;
			//transform.animation.Stop ();
		}
	}
	
	void setTile(GameObject tile_tobeset){
		tile = tile_tobeset;	
	}
	
	
	// backtracking algorithm in progress still
//	bool Find_Path(Vector2 xy){
////		if (x,y is goal) return true;
//		if(xy.x == dest.x && xy.y == dest.z)return true;
////		if (x,y not open) return false;
//		Color tilecolor = Color.blue;
//		foreach (KeyValuePair<Vector2,Color> pair in tiles_dict)
//		{
//			if(pair.Key == xy){
//					tilecolor = pair.Value;
//			}
////			pair.Key;
////			pair.Value)
//		}
//		//GameObject.Find("planes").BroadcastMessage("getXY",xy);
//		
//		if(tile == null){
//			Debug.Log ("dafuq");	
//		}
//		
//		if(tilecolor == Color.red)return false;
////		mark x,y as part of solution path;
//		solutionPath.Add(xy);
//		Vector2 vec = new Vector2(xy.x-1,xy.y);
////		if (FIND-PATH(North of x,y) == true) return true;
//		if(Find_Path (vec)==true)return true;
//		vec = new Vector2(xy.x,xy.y+1);
////		if (FIND-PATH(East of x,y) == true) return true;
//		if(Find_Path (vec)==true)return true;
//		vec = new Vector2(xy.x+1,xy.y);
////		if (FIND-PATH(South of x,y) == true) return true;
//		if(Find_Path (vec)==true)return true;
//		vec = new Vector2(xy.x,xy.y-1);
////		if (FIND-PATH(West of x,y) == true) return true;
//		if(Find_Path (vec)==true)return true;
////		unmark x,y as part of solution path;
//		solutionPath.Remove(xy);
//		return false;
//		
//	}
	
//	IEnumerator AI_move_Controller(Vector3 tile_dest){
//		dest = tile_dest;
//		float i=0;
////		foreach(GameObject ti in tiles){
////			tiles_dict.Add (new Vector2(ti.transform.position.x,ti.transform.position.z),ti.GetComponent<tile>().now);	
////			i++;
////			Debug.Log (i);
////		}
//		
//		
//		//if(Find_Path (new Vector2(player.transform.position.x,player.transform.position.z))){
//			
//			//while(solutionPath.Count != 0 || solutionPath != null){
//			while(i < 1){
//				//move to point
//				i += Time.deltaTime /duration;
//				AI_move (Vector3(solutionPath[i].x,player.transform.position.y,solutionPath[i].y));
//				yield return 0;
//				//solutionPath.Remove(solutionPath[i]); //or Remove(solutionPath[i])
//			}
//			
//			//when backtracking is finished this probably still doesnt work
////			foreach(Vector2 xy in solutionPath){
////				yield return StartCoroutine(AI_move (new Vector3(xy.x,player.transform.position.y,xy.y)));
////				
////				
////			}
//			
//		
//		
//	}
	
	
	
	
//	IEnumerator MoveObj(Transform obj, Vector3 dest, float duration){
//		isMovingToTile = true; // coroutine running
//		Vector3 org = obj.position;
//	
//		float t = 0;
//		while (t < 1){
//			t += Time.deltaTime / duration;
//			obj.position = Vector3.Lerp(org, dest, t);
//			yield return 0;
//		}
//		isMovingToTile = false; // coroutine ended
//	}
	
	
	
	//CLICK TO FOCUS PLAYER
	void OnMouseOver(){
		if(Input.GetMouseButtonDown (0)){
			if(!selected && !GameObject.FindWithTag("Selected_Player")){
				selected = true;
				gameObject.tag = "Selected_Player";
				//Debug.Log ("niggas clickin shit");	
				GameObject.Find ("Camera").SendMessage ("targeted",transform);
				GameObject.Find ("planes").SendMessage ("move", moveableSpaces);
			}
			
			
			//acting
			else if(selected && !selectedToAct && GameObject.FindWithTag ("Selected_Player")== this.gameObject){
				selectedToAct = true;
				GameObject.Find ("planes").SendMessage ("act",castableSpaces);
				
			}
			else if(!selected && GameObject.FindWithTag ("Selected_Player")!= this.gameObject){//NEEDS CHANGING AFTER SKILLS AND GUI
				//im obviously being attacked
				GameObject selPlay = GameObject.FindWithTag ("Selected_Player");
				getHit (selPlay.GetComponent<Player>().damage);
			}
			else if(selected && GameObject.FindWithTag("Selected_Player")== this.gameObject){
				gameObject.tag = "Player";
				GameObject.Find ("Camera").SendMessage ("targeted",transform);
				GameObject.Find ("planes").SendMessage ("disable_ALL");
				selected = false;
				selectedToAct = false;
			}
		}
	}
	
	void rotateCharacterRight(){
		player.Rotate(0,90,0,Space.Self);	
	}
	void rotateCharacterLeft(int degrees=90){
		player.Rotate(0,-degrees,0,Space.Self);	
	}
	
	void punch(int direction){
		//facing and rotation
		
		//falconnn punnnnnch!
		
	}
	
	void getHit(int damage){
		//play hit animation
		Debug.Log ("GET HIT BITCH for "+ damage);
		//do damage to life
		life -= damage;
		Debug.Log ("LIFE: "+life);
		if( life <= 0){
			Destroy(gameObject);	
		}
	}
	
//	void act(GameObject Enemy){
//		if(tile.now == green){
//			Debug.Log ("green!");
//			
//		}
//	}
	
	
}




//		//MOVE CHARACTER
//		if (Input.GetKeyDown ("b") && selected){ //right
//			if(!isMoving){
//				
//				dir=1;
//				distance =1;
//				//Move(3,new Vector3(0,0,1));
//				isMoving = true;
//				movDirection = player.forward;
//				//Debug.Log ((int)movDirection.x);
//				//calc dir
//				int x = (int)movDirection.x*2;
//				int z = (int)movDirection.z*1;
//				dir = x +z;  //if dir = 1 right  2 down  -1 left  -2 down
//				//Debug.Log ("Dir "+dir);
//				
//				//Debug.Log (movDirection);
//				playerpos = player.position;
//				dest = playerpos + (movDirection)*distance; // * number of blocks
//				//Debug.Log ("player moving in " + player.forward );
//			}
//			
//			
//		}
		
//		if (isMoving){
//			
//			player.Translate (new Vector3(0,0,1) *Time.deltaTime);
//			if(dir ==1 && player.position.z >= dest.z){
//				isMoving =false;Debug.Log ("1");
//			}else if(dir == 2 && player.position.x >= dest.x){
//				isMoving =false;Debug.Log ("2");
//			}else if(dir == -1 && player.position.z <= dest.z){
//				isMoving =false;Debug.Log ("-1");
//			}else if(dir == -2 && player.position.x <= dest.x){
//				isMoving =false;Debug.Log ("-2");
//			}
//			
//			if(!isMoving){
//				player.position = dest;	
//			}
//		}
