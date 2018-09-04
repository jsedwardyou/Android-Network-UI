using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Touch : MonoBehaviour {

    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject touch_circle;

    [SerializeField] private float speed;

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
                Vector2 direction = hit.transform.position - wing.transform.position;

                float angle = Vector2.SignedAngle(Vector2.right, direction);
                wing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
	}

    private IEnumerator move_to_pos(GameObject emoticon) {

        yield return null;
    }
}
