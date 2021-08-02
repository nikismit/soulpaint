using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVol : MonoBehaviour
{
    AudioSource _mainSound;
    public float[] _samples = new float[1024];
    public float volume;

    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        _mainSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _mainSound.GetSpectrumData(_samples, 1, FFTWindow.Rectangular);
        isPlaying = _mainSound.isPlaying;
        Volume();
    }

    void Volume()
    {
        volume = 0;
        foreach (float s in _samples)
        {
            volume += s;
        }
    }
}
