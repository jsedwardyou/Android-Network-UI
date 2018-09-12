using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Touch : MonoBehaviour {

    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject touch_circle;
    [SerializeField] private GameObject mini_circle;
    [SerializeField] private GameObject message_sent;
    private Vector3 initial_circle_pos;
    public NetworkClient_UI network;
    public GameObject received_em;
    public GameObject cars;

    [SerializeField] private emoticon em;
    [SerializeField] private touch_circle circle;

    [SerializeField] private float movement_speed;

    [SerializeField] private Text message;

    private bool trigger_emoji = true;
    private bool can_select = true;

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
        //Received Message
        if (network.car_message != "") {
            string[] message = network.car_message.Split(',');
            int car_id = int.Parse(message[0]);
            int num = int.Parse(message[1]);
            for (int i = 0; i < cars.transform.childCount; i++) {
                if (!cars.transform.GetChild(i).gameObject.activeInHierarchy) continue;

                if (cars.transform.GetChild(i).GetComponent<Car_ID>().car_id == car_id) {
                    m_current_car = cars.transform.GetChild(i).gameObject;
                    m_current_car.GetComponent<Car_Selection>().Change_to_selected();
                }
                Debug.Log(car_id);
            }
            GameObject emoticon = received_em.transform.GetChild(num).gameObject;
            StartCoroutine(Message_Received(emoticon));
        }

        if (Input.GetMouseButtonDown(0) && can_select)
        {
            if (!hit) return;
            if (hit.transform.tag == "emoticon")
            {
                m_current_target = hit.transform.gameObject;
            }
            else if (hit.transform.tag == "car") {
                if (m_current_car != null) {
                    m_current_car.GetComponent<Car_Selection>().Change_to_not_selected();
                }
                m_current_car = hit.transform.gameObject;
                m_current_car.GetComponent<Car_Selection>().Change_to_selected();
            }
        }

        if (m_current_car == null) {
            em.Current_State(false);
            circle.Current_State(false);
            return;
        }
        em.Current_State(true);
        circle.Current_State(true);
        if (m_current_target == null) return;
        //Vector2 direction = touch_circle.transform.position - wing.transform.position;
        //float angle = Vector2.SignedAngle(Vector2.right, direction);
        //wing.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (trigger_emoji == false) return;

        if (hit.transform.name == "record")
        {
            StartCoroutine(voice_input());
        }
        else {
            StartCoroutine(Car_Interaction());
        }
    }

    private IEnumerator Car_Interaction() {
        trigger_emoji = false;
        can_select = false;
        Vector3 initial_emoji_pos = m_current_target.transform.position;
        //Move the touch_circle to emoji's position
        touch_circle.transform.position = m_current_target.transform.position;
        m_current_target.transform.SetParent(touch_circle.transform);

        float distance = Vector2.Distance(touch_circle.transform.position, mini_circle.transform.position);
        while (distance > 0.05f)
        {
            touch_circle.transform.position = Vector2.MoveTowards(touch_circle.transform.position, mini_circle.transform.position, movement_speed * Time.deltaTime);
            distance = Vector2.Distance(touch_circle.transform.position, mini_circle.transform.position);
            yield return null;
        }
        message_sent.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        message_sent.SetActive(false);
        Reset_UI(initial_emoji_pos);
        trigger_emoji = true;
        can_select = true;
        yield return null;
    }

    [SerializeField] private voice_input voice;
    private IEnumerator voice_input() {
        trigger_emoji = false;
        Vector3 initial_emoji_pos = m_current_target.transform.position;
        touch_circle.transform.position = m_current_target.transform.position;
        //Detect user's voice
        //voice.turn_on_mic();
        //while (voice.Audio_Source.isPlaying) {
        //    yield return null;
        //}
        yield return new WaitForSeconds(5.0f);
        message_sent.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        message_sent.SetActive(false);
        Reset_UI(initial_emoji_pos);
        trigger_emoji = true;
        can_select = true;
        yield return null;
    }

    private IEnumerator Message_Received(GameObject obj) {
        obj.SetActive(true);
        obj.transform.position = circle.transform.position;
        yield return new WaitForSeconds(3.0f);

        obj.SetActive(false);

        yield return null;
    }

    private void Reset_UI(Vector3 initial_pos) {
        m_current_car.GetComponent<Car_Selection>().Change_to_not_selected();
        m_current_target.transform.SetParent(em.transform);
        m_current_target.transform.position = initial_pos;
        m_current_car = null;
        m_current_target = null;
        touch_circle.transform.position = initial_circle_pos;
        em.Current_State(false);
    }
}
