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

        public float cameraSize = 4f;
        private bool zoom = true;

        void Start()
        {
            Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        }

        void FixedUpdate()
        {
            //Fazer com que a camera mova suavemente para o Player
            if (target != null)
            {
                Vector3 desirePosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }

            //Dar zoom in para 4.0f
            if(zoom && cameraSize > 4.0f)
            {
                cameraSize -= Time.deltaTime;
            }

            //Dar zoom out para 4.0f
            if(!zoom && cameraSize < 6.0f)
            {
                cameraSize += Time.deltaTime;
            }
            gameObject.GetComponent<Camera>().orthographicSize = cameraSize;
        }

        [YarnCommand("zoom")]
        public void Zoom(string mode)
        {
            if (mode == "in")
            {
                zoom = true;
            }
            if (mode == "out")
            {
                zoom = false;
            }
        }
    }
}


