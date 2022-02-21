using Shared;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScorePanel : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        [ReadOnlyField] public int score;

        public void UpdateScore(int points)
        {
            score += points;
            scoreText.text = score.ToString();
        }
    }
}