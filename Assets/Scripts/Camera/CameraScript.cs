using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class CameraScript : MonoBehaviour
    {
        //array para guardar todos os alvos da Camera durante o dialogo
        [System.Serializable]
        public struct TargetInfo
        {
            public string name;
            public Transform moveTo;
        }
        public TargetInfo[] targets;

        public string targetName;
        private float smoothSpeed;
        public Vector3 offset;

        public float cameraSize = 4f;
        private bool zoom = true;

        void Start()
        {
            Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
            smoothSpeed = 5f;
        }

        void FixedUpdate()
        {
            //Fazer com que a camera mova suavemente para o Player
            if (targets != null)
            {
                foreach(var target in targets)
                {
                    if (target.name.Equals(targetName))
                    {
                        Vector3 desirePosition = target.moveTo.position + offset;
                        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);
                        transform.position = smoothedPosition;
                        break;
                    }
                }
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

        [YarnCommand("moveTo")]
        public void MoveTo(string commandTarget)
        {
            string targetTest = null;
            
            //Procura o target dentro do array
            foreach (var target in targets)
            {
                if (target.name.Equals(commandTarget))
                {
                    targetTest = commandTarget;
                    break;
                }
            }

            //Se não achar o target retorna e mostre um error
            if(targetTest == null)
            {
                Debug.LogErrorFormat("Não foi encontrando o target {0}!", commandTarget);
                return;
            }

            //se achar diga qual o nome do target para a camera ir
            targetName = targetTest;
        }
    }
}


