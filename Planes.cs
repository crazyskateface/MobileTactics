using UnityEngine;
using System.Collections;

public class Planes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	void move(int m){
		BroadcastMessage ("moveable",m);
	}
	
	void act(){
		BroadcastMessage ("castable");	
	}
	
	void disable_ALL(){
		BroadcastMessage ("disable");	
	}
	
}
