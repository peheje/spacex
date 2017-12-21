using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject following;
    private Vector3 offset;

    void Start () {
        offset = transform.position - following.transform.position;
	}
	
	void LateUpdate () {
        transform.position = following.transform.position + offset;
	}
}
