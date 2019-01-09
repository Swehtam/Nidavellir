using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player)
        {
            if (player.gameObject.GetComponent<PlayerHealthManager>().died == true)
            {
                StartCoroutine(GoToScene(2.5f, "DeathScene"));
            }
        }
    }

    public IEnumerator GoToScene(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
