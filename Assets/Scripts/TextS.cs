using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextS : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        Color color = GetComponent<Image>().color;
        color.a = 0.5f;
        GetComponent<Image>().color = color;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
