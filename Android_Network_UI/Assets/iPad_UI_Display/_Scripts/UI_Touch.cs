using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Touch : MonoBehaviour {

    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject touch_circle;
    private Vector3 initial_circle_pos;


    [SerializeField] private emoticon em;
    [SerializeField] private touch_circle circle;

    [SerializeField] private float movement_speed;

    [SerializeField] private GameObject bluebox;
    [SerializeField] private Text message;

    private bool trigger_emoji = true;

    private GameObject m_current_car;
    public GameObject current_car
    {
        get
        {
            return m_current_car;
        }
        set{
            m_current_car = value;
        }

    }

    private GameObject m_current_target;
    public GameObject Current_target {
        get {
            return m_current_target;
        }
    }

	// Use this for initialization
	void Start () {
        initial_circle_pos = touch_circle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            if (!hit) return;
            if (hit.transform.tag == "emoticon")
            {
                m_current_target = hit.transform.gameObject;
            }
            else if (hit.transform.tag == "car") {
                m_current_car = hit.transform.gameObject;
            }
        }

        if (m_current_car == null) {
            em.Current_State(false);
            circle.Current_State(false);
            bluebox.SetActive(false);
            return;
        }
        bluebox.SetActive(true);
        em.Current_State(true);
        circle.Current_State(true);

        bluebox.transform.position = m_current_car.transform.position;

        if (m_current_target == null) return;
        Vector2 direction = touch_circle.transform.position - wing.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        wing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (trigger_emoji == false) return;
        StartCoroutine(Car_Interaction());
        
    }

    private IEnumerator Car_Interaction() {
        trigger_emoji = false;
        float distance = Vector2.Distance(m_current_target.transform.position, touch_circle.transform.position);
        while (distance > 0.1f)
        {
            touch_circle.transform.position = Vector2.MoveTowards(touch_circle.transform.position, m_current_target.transform.position, movement_speed * Time.deltaTime);
            distance = Vector2.Distance(m_current_target.transform.position, touch_circle.transform.position);
            yield return null;
        }
        message.text = "메세지가 성공적으로 전달되었습니다";
        yield return new WaitForSeconds(2.0f);
        message.text = "";
        Reset_UI();
        trigger_emoji = true;
        yield return null;
    }

    private void Reset_UI() {
        m_current_car = null;
        m_current_target = null;
        touch_circle.transform.position = initial_circle_pos;
        wing.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
