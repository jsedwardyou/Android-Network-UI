using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voice_visualization : MonoBehaviour {

    public GameObject square_box_pivot;
    private GameObject[] square_box = new GameObject[512];
    private Vector2[] initial_position = new Vector2[512];
    private AudioSource audio_source;
    private voice_input voice_input;

    private float max_scale = 10;

	// Use this for initialization
	void Start () {
        audio_source = GetComponent<AudioSource>();
        voice_input = GetComponent<voice_input>();
        for (int i = 0; i < 512; i++) {
            GameObject box_pivot = Instantiate(square_box_pivot);
            box_pivot.transform.SetParent(this.transform);
            box_pivot.transform.position = transform.position;
            box_pivot.transform.GetChild(0).localPosition += new Vector3(1.5f, 0, 0);
            box_pivot.transform.eulerAngles = new Vector3(0,0, i * 0.703125f);

            square_box[i] = box_pivot;
            initial_position[i] = box_pivot.transform.GetChild(0).localPosition;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (audio_source.isPlaying)
        {
            for (int i = 0; i < 512; i++)
            {
                if (square_box[i] != null)
                {
                    square_box[i].SetActive(true);
                    Transform square = square_box[i].transform.GetChild(0);
                    square.localScale = new Vector2(voice_input._samples[i] * max_scale, 0.1f);
                    square.localPosition = initial_position[i] + new Vector2(square.localScale.x / 2, 0);
                }
            }
        }
        else {
            for (int i = 0; i < 512; i++)
            {
                if (square_box[i] != null)
                {
                    square_box[i].SetActive(false);
                }
            }
        }
	}
}
