using System.Collections;
using UnityEngine;

namespace Assets.Scrips.Semana1
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score;
        public int BestScore;

        public void UpdateScore(int value)
        {
            Score += value;
            if (Score > BestScore)
            {
                BestScore = Score;
            }
        }
    }
}