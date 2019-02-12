using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class Phase4Script : MonoBehaviour
    {
        public GameObject leftWall;
        public GameObject rightWall;

        [YarnCommand("ShowWall")]
        public void ShowWall(string side)
        {
            if (side.Equals("Left"))
            {
                leftWall.SetActive(true);
            }
            else if (side.Equals("Right"))
            {
                rightWall.SetActive(true);
            }
        }

        [YarnCommand("HideWall")]
        public void HideWall(string side)
        {
            if (side.Equals("Left"))
            {
                leftWall.SetActive(false);
            }
            else if (side.Equals("Right"))
            {
                rightWall.SetActive(false);
            }
        }
    }
}
