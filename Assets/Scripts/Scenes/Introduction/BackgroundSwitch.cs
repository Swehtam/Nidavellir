using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yarn.Unity.Example
{
    public class BackgroundSwitch : MonoBehaviour
    {
        [System.Serializable]
        public struct BackgroundsInfo
        {
            public string name;
            public Sprite backgroundSprite;
        }

        public BackgroundsInfo[] backgrounds;

        [YarnCommand("change")]
        public void ChangeSprite(string spriteName)
        {
            bool backgroundFound = false;
            //procura o background certo
            foreach (var info in backgrounds)
            {
                if (info.name == spriteName)
                {
                    GetComponent<SpriteRenderer>().sprite = info.backgroundSprite;
                    backgroundFound = true;
                    break;
                }
            }

            //se não achar mandar uma mensagem para o console
            if (!backgroundFound)
            {
                Debug.LogErrorFormat("Não foi encontrando o background {0}!", backgroundFound);
                return;
            }
        }
    }
}