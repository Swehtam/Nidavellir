using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class CameraScript : MonoBehaviour
    {
        public Transform target;

        public float smoothSpeed;
        public Vector3 offset;

        void Start()
        {
            Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 desirePosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
            gameObject.GetComponent<Camera>().orthographicSize = 1.0f;
        }

        [YarnCommand("zoom")]
        public void Zoom(string mode)
        {
            if (mode == "in")
            {

            }
        }
    }
}


