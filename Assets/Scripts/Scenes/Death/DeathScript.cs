using UnityEngine;

namespace Yarn.Unity.Example
{
    public class DeathScript : MonoBehaviour
    {
        private string levelName;
        private LevelChanger levelChanger;

        void Start()
        {
            levelChanger = FindObjectOfType<LevelChanger>();
        }

        // Metodo para Salvar e Carregar a cena do menu quando player aperta o botão
        public void LoadMenuScene()
        {
            // ----Fazer e colocar aqui o metodo para salvar o jogo quando morrer----
            levelChanger.FadeToLevel("Menu");
        }

        // Metodo para voltar para o save do player, quando ele apertar esse botão
        public void LoadGameScene()
        {
            // ----Colocar aqui o metodo para voltar para o save do player, por enquanto volta para a cene inicial----
            levelName = PlayerDeath.scene;
            levelChanger.FadeToLevel(levelName);
        }
    }
}