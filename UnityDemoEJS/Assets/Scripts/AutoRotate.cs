using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public float rotationSpeed = 20;
    public Vector3 rotationVector = new Vector3(1,0,0);
	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        // Rotate the object around its local X axis at rotationSpeed degrees per second
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
