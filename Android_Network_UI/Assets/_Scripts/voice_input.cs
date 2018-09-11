using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voice_input : MonoBehaviour {

    [SerializeField] private int mic_length;

    private AudioSource m_audio_source;
    public AudioSource Audio_Source {
        get {
            return m_audio_source;
        }
    }
    public float[] _samples = new float[512];
	// Use this for initialization
	void Start () {
        m_audio_source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
        GetSpectrumAudioSource();
    }

    private void GetSpectrumAudioSource() {
        m_audio_source.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    public void turn_on_mic() {
        
        m_audio_source.clip = Microphone.Start(null, false, mic_length, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        m_audio_source.Play();
    }

    public void check_audio_source() {
        
    }
}
