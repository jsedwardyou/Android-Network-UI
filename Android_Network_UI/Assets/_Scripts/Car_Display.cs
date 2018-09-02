using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Display : MonoBehaviour {

    [SerializeField] private GameObject surrounding_cars;

    private GameObject[] cars = new GameObject[4];

    [SerializeField] NetworkClient_UI client;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < cars.Length; i++) {
            cars[i] = surrounding_cars.transform.GetChild(i).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < cars.Length; i++) {
            if (client.GetState(i) == 0)
            {
                cars[i].SetActive(false);
            }
            else if (client.GetState(i) == 1) {
                cars[i].SetActive(true);
            }
        }
	}
}
