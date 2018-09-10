using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Selection : MonoBehaviour {

    public Sprite selected;
    public Sprite not_selected;

    public void Change_to_selected() {
        GetComponent<SpriteRenderer>().sprite = selected;
    }

    public void Change_to_not_selected()
    {
        GetComponent<SpriteRenderer>().sprite = not_selected;
    }

}
