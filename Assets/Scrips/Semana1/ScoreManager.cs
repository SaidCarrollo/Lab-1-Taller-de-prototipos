using System.Collections;
using UnityEngine;

namespace Assets.Scrips.Semana1
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score;
        public int BestScore;
        public UiManager uiManager;

        private void Awake()
        {
            // Cargar el mejor puntaje guardado al iniciar
            BestScore = PlayerPrefs.GetInt("BestScore", 0);
            uiManager.UpdateBestScore();
        }

        public void UpdateScore(int value)
        {
            Score += value;

            if (Score > BestScore)
            {
                BestScore = Score;
                PlayerPrefs.SetInt("BestScore", BestScore); // Guardar nuevo mejor puntaje
                PlayerPrefs.Save();
            }

            uiManager.UpdateActualScore();
            uiManager.UpdateBestScore();
        }
    }
}