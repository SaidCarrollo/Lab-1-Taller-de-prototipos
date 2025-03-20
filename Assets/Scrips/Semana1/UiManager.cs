using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrips.Semana1
{
    public class UiManager : MonoBehaviour
    {
        public Text ActualScore;
        public Text BestScoreT;
        public ScoreManager scoreManager;

        public void UpdateActualScore()
        {
            ActualScore.text = "Score: " + scoreManager.Score.ToString();
        }

        public void UpdateBestScore()
        {
            BestScoreT.text = "Best Score: " + scoreManager.BestScore.ToString();
        }
    }
}