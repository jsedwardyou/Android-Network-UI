using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Touch : MonoBehaviour {

    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject touch_circle;

    [SerializeField] private float speed;

    private GameObject m_current_target;
    public GameObject Current_target {
        get {
            return m_current_target;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0)) {
            if (!hit) return;
            if (hit.transform.tag == "emoticon") {
                m_current_target = hit.transform.gameObject;
            }
        }
        Vector2 direction = touch_circle.transform.position - wing.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        wing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
