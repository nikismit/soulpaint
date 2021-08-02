using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangerAudioFile : MonoBehaviour
{
    public bool ScaleMe;
    public float audioMinVol = 0, audioMaxVol = 1, minScaleTLocal = .5f, maxScaleTLocal = 1.5f;
   public AudioVol audioVol;
  //[SerializeField]  AudioSource audioVol;
    public Vector3[] startScale, convertedMic;
    public Transform[] objectsToScale;

    private float old_val, old_min, old_max;
    private Vector3[] new_min, new_max;

    private float speed, maxSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        objectsToScale = GetComponentsInChildren<Transform>();

        convertedMic = new Vector3[objectsToScale.Length];
        new_min = new Vector3[objectsToScale.Length];
        new_max = new Vector3[objectsToScale.Length];
        startScale = new Vector3[objectsToScale.Length];

        for (int x = 1; x < objectsToScale.Length; x++)
        {
            startScale[x] = objectsToScale[x].transform.localScale;
        }

        for (int i = 0; i < objectsToScale.Length; i++)
        {
            new_min[i] = new Vector3(objectsToScale[i].transform.localScale.x * minScaleTLocal, objectsToScale[i].transform.localScale.y * minScaleTLocal, objectsToScale[i].transform.localScale.z * minScaleTLocal);
            new_max[i] = new Vector3(objectsToScale[i].transform.localScale.x * maxScaleTLocal, objectsToScale[i].transform.localScale.y * maxScaleTLocal, objectsToScale[i].transform.localScale.z * maxScaleTLocal);
        }

        old_min = audioMinVol;
        old_max = audioMaxVol;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleMe = audioVol.isPlaying;

    

        if (ScaleMe)
        {
            speed = 0;
            for (int i = 1; i < objectsToScale.Length; i++)
            {
                convertedMic[i].x = ((audioVol.volume - old_min) / (old_max - old_min)) * (new_max[i].x - new_min[i].x) + new_min[i].x;
                convertedMic[i].y = ((audioVol.volume - old_min) / (old_max - old_min)) * (new_max[i].y - new_min[i].y) + new_min[i].y;
                convertedMic[i].z = ((audioVol.volume - old_min) / (old_max - old_min)) * (new_max[i].z - new_min[i].z) + new_min[i].z;

                if (convertedMic[i].x < new_min[i].x || convertedMic[i].y < new_min[i].y || convertedMic[i].z < new_min[i].z)
                {
                    convertedMic[i] = new_min[i];
                }
                if (convertedMic[i].x > new_max[i].x || convertedMic[i].y > new_max[i].y || convertedMic[i].z > new_max[i].z)
                {
                    convertedMic[i] = new_max[i];
                }

                objectsToScale[i].localScale = Vector3.Lerp(objectsToScale[i].localScale, convertedMic[i], Time.deltaTime);
            }
        }
        else
        {
            if (speed < maxSpeed)
            {
                speed += .001f;
            }
            else
            {
                speed = maxSpeed;
            }
            for (int i = 1; i < objectsToScale.Length; i++)
            {
                objectsToScale[i].localScale = Vector3.Lerp(objectsToScale[i].localScale, startScale[i], speed * Time.deltaTime);
            }
        }
    }
}