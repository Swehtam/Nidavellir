using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yarn.Unity.Example
{
    public class HUDController : MonoBehaviour
    {

        public Sprite[] hPSprites;
        public Sprite[] cervaSprites;

        public Image hPUI;
        public Image cervaUI;

        public GameObject player;
        public GameObject key;
        public BoneDragonController boss;

        // Update is called once per frame
        void Update()
        {
            int hp = player.GetComponent<PlayerHealthManager>().currentHealth;

            switch (hp)
            {
                case 0:
                    hPUI.sprite = hPSprites[0];
                    break;
                case 1:
                    hPUI.sprite = hPSprites[1];
                    break;
                case 2:
                    hPUI.sprite = hPSprites[2];
                    break;
                case 3:
                    hPUI.sprite = hPSprites[3];
                    break;
                case 4:
                    hPUI.sprite = hPSprites[4];
                    break;
                case 5:
                    hPUI.sprite = hPSprites[5];
                    break;
            }

            if (key)
            {
                int honra = key.GetComponent<KeyController>().onda;
                switch (honra)
                {
                    case -1:
                        cervaUI.sprite = cervaSprites[0];
                        break;
                    case 0:
                        cervaUI.sprite = cervaSprites[0];
                        break;
                    case 1:
                        cervaUI.sprite = cervaSprites[0];
                        break;
                    case 2:
                        cervaUI.sprite = cervaSprites[1];
                        break;
                    case 3:
                        cervaUI.sprite = cervaSprites[2];
                        break;
                    case 4:
                        cervaUI.sprite = cervaSprites[3];
                        break;
                    case 5:
                        cervaUI.sprite = cervaSprites[4];
                        break;
                    case 6:
                        cervaUI.sprite = cervaSprites[5];
                        break;
                }
            }

            if (boss)
            {
                int honra = boss.phase;
                switch (honra)
                {
                    case 1:
                        cervaUI.sprite = cervaSprites[0];
                        break;
                    case 2:
                        cervaUI.sprite = cervaSprites[2];
                        break;
                    case 3:
                        cervaUI.sprite = cervaSprites[4];
                        break;
                    case 4:
                        cervaUI.sprite = cervaSprites[5];
                        break;
                }
            }

        }
    }
}