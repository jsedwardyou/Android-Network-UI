using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emoticon : MonoBehaviour {

    private SpriteRenderer[] emoticons = new SpriteRenderer[3];

    private bool visible = false;

    private void Start()
    {
        for (int i = 0; i < emoticons.Length; i++) {
            emoticons[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (visible)
        {
            foreach (SpriteRenderer emoticon in emoticons)
            {
                emoticon.color = new Vector4(1, 1, 1, 1);
                emoticon.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        else {
            foreach (SpriteRenderer emoticon in emoticons)
            {
                emoticon.color = new Vector4(0,0,0,0);
                emoticon.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
	}

    public void Current_State(bool state) {
        visible = state;
    }
}
