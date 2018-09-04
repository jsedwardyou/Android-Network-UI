using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_circle : MonoBehaviour {

    [SerializeField] float changing_speed;

    private SpriteRenderer sprite;
    private Color initial_color;

    private bool start_blinking = false;

    private Vector2 initial_position;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        initial_color = sprite.color;
        initial_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (start_blinking)
        {
            float changing_alpha = Mathf.Sin(Time.fixedTime * changing_speed) * 0.5f + 0.5f;
            Vector4 new_color = new Vector4(initial_color.r, initial_color.g, initial_color.b, changing_alpha);

            sprite.color = new_color;
        }
        else {
            sprite.color = new Vector4(0, 0, 0, 0);
        }
	}

    public void Current_State(bool state) {
        start_blinking = state;
    }
}
