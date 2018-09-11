using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Display : MonoBehaviour {

    [Header("Car UI")]
    public GameObject car_ui;
    [SerializeField] private float car_ui_size;

    [SerializeField] private GameObject driving_car;
    [SerializeField] private int initial_pool;
    [SerializeField] private Transform car_parent;

    private List<GameObject> car_pool = new List<GameObject>();

    [SerializeField] NetworkClient_UI client;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < initial_pool; i++) {
            GameObject spawned_car_ui = Instantiate(car_ui, Vector2.zero, Quaternion.identity);
            spawned_car_ui.transform.localScale = new Vector3(car_ui_size, car_ui_size, car_ui_size);
            spawned_car_ui.SetActive(false);
            spawned_car_ui.transform.SetParent(car_parent);
            car_pool.Add(spawned_car_ui);
        }
	}
	
	// Update is called once per frame
	void Update () {
        string[] car = client.car;
        if (car.Length < 2) {
            return;
        }
        int car_num = client.car.Length - 2;
        string[] car_name = new string[car_num];
        string[] car_pos  = new string[car_num];
        string[] car_rot  = new string[car_num];
        string[] car_id = new string[car_num];

        for (int i = 1; i < car.Length - 1; i++)
        {
            string[] car_details = car[i].Split('^');
            car_name[i - 1] = car_details[0];
            car_pos[i - 1] = car_details[1];
            car_rot[i - 1] = car_details[2];
            car_id[i - 1] = car_details[3];
        }

        if (car_num > car_pool.Count)
        {
            for (int i = 0; i < car_num - car_pool.Count; i++)
            {
                GameObject spawned_car_ui = Instantiate(car_ui, Vector2.zero, Quaternion.identity);
                spawned_car_ui.transform.localScale = new Vector3(car_ui_size, car_ui_size, car_ui_size);
                spawned_car_ui.SetActive(false);
                spawned_car_ui.transform.SetParent(car_parent);
                car_pool.Add(spawned_car_ui);
            }
        }
        else {
            for (int i = 0; i < car_num; i++) {
                car_pool[i].SetActive(true);
                float x_pos = float.Parse(car_pos[i].Split(',')[0]);
                float y_pos = float.Parse(car_pos[i].Split(',')[1]);
                Vector2 new_pos = new Vector2(driving_car.transform.position.x - x_pos, driving_car.transform.position.y - y_pos);
                car_pool[i].transform.localPosition = new_pos - (Vector2)car_parent.transform.position;
                car_pool[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -float.Parse(car_rot[i])));
                car_parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, float.Parse(car_name[i])));
                car_pool[i].GetComponent<Car_ID>().car_id = int.Parse(car_id[i]);
            }
            for (int i = car_num; i < car_pool.Count; i++) {
                car_pool[i].SetActive(false);
            }
        }


        
    }
}
