using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

    public emoticon em;
    public touch_circle circle;

    public bool state;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            em.Current_State(state);
            circle.Current_State(state);
        
	}
}
