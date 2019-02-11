using UnityEngine;

namespace Yarn.Unity.Example
{
    public class LevelChanger : MonoBehaviour
    {
        public Animator anim;
        private string levelToLoad;

        public void FadeToLevel(string levelName)
        {
            levelToLoad = levelName;
            anim.SetBool("FadeOut", true);
        }

        public void OnFadeComplete()
        {
            LoadingScreenManager.LoadScene(levelToLoad);
        }
    }
}

