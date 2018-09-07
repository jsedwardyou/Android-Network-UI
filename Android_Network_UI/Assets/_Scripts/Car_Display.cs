using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Display : MonoBehaviour {

    [SerializeField] private GameObject surrounding_cars;
    [SerializeField] private GameObject driving_car;

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

        string[] car = client.car;
        if (car.Length < 2) {
            return;
        }
        int car_num = client.car.Length - 2;
        string[] car_name = new string[car_num];
        string[] car_pos  = new string[car_num];
        string[] car_rot  = new string[car_num];

        for (int i = 1; i < car.Length - 1; i++)
        {
            string[] car_details = car[i].Split('^');
            car_name[i - 1] = car_details[0];
            car_pos[i - 1] = car_details[1];
            car_rot[i - 1] = car_details[2];
        }
        for (int i = 0; i < car_num; i++) {
            Debug.Log(car_num);
            cars[i].SetActive(true);
            float x_pos = float.Parse(car_pos[i].Split(',')[0]);
            float y_pos = float.Parse(car_pos[i].Split(',')[1]);
            Vector2 new_pos = new Vector2(driving_car.transform.position.x - x_pos, driving_car.transform.position.y - y_pos);
            cars[i].transform.position = new_pos;
            cars[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -float.Parse(car_rot[i])-90));
        }
        
    }
}
