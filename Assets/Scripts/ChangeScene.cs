﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
            Debug.Log("Tried loading scene");
        }

    }



  private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandController"))
        {
            SceneManager.LoadScene(1);
            }
        }
    }




