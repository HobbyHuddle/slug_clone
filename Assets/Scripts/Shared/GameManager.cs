using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shared
{
    public class GameManager : MonoBehaviour
    {
        public string currentScene;
        
        public void ResetGame()
        {
            SceneManager.LoadSceneAsync(currentScene);
        }
    }
}