using Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shared
{
    public class GameManager : MonoBehaviour
    {
        public string currentScene;
        public PlayerCharacter player1;
        
        public void ResetGame()
        {
            SceneManager.LoadSceneAsync(currentScene);
        }

        public void HealthBoost()
        {
            player1.health += 50;
        }
    }
}