using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed;
    public Vector3 offset;

    void Start()
    {
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
    }

    void FixedUpdate () {
        if(target != null)
        {
            Vector3 desirePosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
		
	}
}
