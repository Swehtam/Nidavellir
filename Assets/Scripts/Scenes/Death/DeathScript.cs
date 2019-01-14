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

        LoadingScreenManager.LoadScene("Menu");
    }

    // Metodo para voltar para o save do player, quando ele apertar esse botão
    public void LoadGameScene()
    {
        // ----Colocar aqui o metodo para voltar para o save do player, por enquanto volta para a cene inicial----

        LoadingScreenManager.LoadScene("Scene2D");
    }
}
