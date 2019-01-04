using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    // Metodo para Salvar e Carregar a cena do menu quando player aperta o botão
    public void LoadMenuScene()
    {
        // ----Fazer e colocar aqui o metodo para salvar o jogo quando morrer----

        StartCoroutine(GoToScene(1.0f, "Menu"));
    }

    // Metodo para voltar para o save do player, quando ele apertar esse botão
    public void LoadGameScene()
    {
        // ----Colocar aqui o metodo para voltar para o save do player, por enquanto volta para a cene inicial----

        StartCoroutine(GoToScene(1.0f, "Scene2D"));
    }

    public IEnumerator GoToScene(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
