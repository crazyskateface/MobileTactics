using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	//colors
	public Color none = Color.grey;
	public Color red = Color.red;
	public Color blue= Color.blue;
	public Color green=Color.green;
	public Color now;
	
	//enabled as in visible
	public bool enabled = false;
	
	//all player objects in game
	GameObject[] players;
	//all enemy objects in game
	GameObject[] enemies;
	//xy pos of current tile   [x, y] vector2
	public Vector2 xy;
	
	
	
	
	// Use this for initialization
	void Start () {
		xy = new Vector2(transform.position.x, transform.position.z);
		now = red;
		renderer.material.color = red;
		enabled = false;
		gameObject.layer = 13;
		players = GameObject.FindGameObjectsWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		
		if(enabled)
			renderer.enabled = true;
		else {
			renderer.enabled = false;
		}
	}
	void enable(bool enable){
		enabled = enable;
	}
	//when tile is moused over 
	void OnMouseOver(){
		//if tile is clicked
		if(Input.GetMouseButtonDown (0)){
			//if tile is blue and moveable
			//     then get the player object and send a message to the AI_Move method on the player
			//     giving it this tile's current position to move to.
			if(now == blue){
				GameObject selected_player = GameObject.FindGameObjectWithTag("Selected_Player");
				if(selected_player){
						//selected_player.SendMessage ("AI_move_Controller",transform.position);
						selected_player.SendMessage ("AI_move",transform.position);
				}
			}
		}
	}
	
	
	
	//if this tile should be able to be 'moved onto' then this will fire and determine if it can be moved onto and change to blue
	void moveable(int moveableSpaces){
		enable (true);
		
		//get distance from player aka math!
		Transform player = GameObject.FindGameObjectWithTag ("Selected_Player").transform;
		float dist= Vector3.Distance (transform.position, player.position);
		int zdiff = (int)Mathf.Abs (player.position.z - transform.position.z);
		int xdiff = (int)Mathf.Abs (player.position.x - transform.position.x);
		int diff = (int)Mathf.Abs ((int)Mathf.Abs (zdiff)*(int)Mathf.Abs (xdiff));
		
		if(diff == 0)
			diff +=2;
		int MS = moveableSpaces - diff;
		Debug.Log(dist);
		dist = (int)dist;
		if(dist <= (MS+2)){
			//float lerp = Mathf.PingPong(Time.time, 1.0F)/1.0F;
			GameObject[] playerTagged = GameObject.FindGameObjectsWithTag("Player");
			GameObject[] wallTagged= GameObject.FindGameObjectsWithTag("wall");
			bool blocked = false;
			foreach(GameObject ob in playerTagged){
				if(ob.transform.position.x == transform.position.x && ob.transform.position.z == transform.position.z){
					blocked = true;	
				}
			}
			foreach(GameObject ob in wallTagged){
				if(ob.transform.position.x == transform.position.x && ob.transform.position.z == transform.position.z){
					blocked = true;	
				}
			}
			if(!blocked){
				renderer.material.color = blue;
				now = blue;
				gameObject.layer = 11;
				
			}else{
				now = red;	
				gameObject.layer = 10;
			}
			
		}
	}
	
	
	
	//determines if this tile can be a castable tile and turn green
	void castable(){
		enable (true);
		
		//get distance from player aka math!
		Transform player = GameObject.FindGameObjectWithTag ("Selected_Player").transform;
		float dist= Vector3.Distance (transform.position, player.position);
		int zdiff = (int)Mathf.Abs (player.position.z - transform.position.z);
		int xdiff = (int)Mathf.Abs (player.position.x - transform.position.x);
		int diff = (int)Mathf.Abs ((int)Mathf.Abs (zdiff)*(int)Mathf.Abs (xdiff));
		
		int MS = 1 - diff;
		Debug.Log(dist);
		dist = (int)dist;
		if(dist <= (MS)){
			//float lerp = Mathf.PingPong(Time.time, 1.0F)/1.0F;
			renderer.material.color = green;
			now = green;
			gameObject.layer = 12;
			
		}else{
			renderer.material.color = red;
			now = red;
			gameObject.layer = 10;
		}
	}
	void disable(){
		renderer.material.color = red;
		now = red;
		enable (false);	
		gameObject.layer = 13;
	}
	
	void getXY(Vector2 pxy){
		if(pxy == xy){
			GameObject.FindWithTag ("Selected_Player").SendMessage("setTile",gameObject);	
		}
	}
	
	
}
