using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

namespace VWPrototype
{
    public class VacuumGunScoreFunction : AVacuumGunFunction
    {
        [SerializeField]
        private InputActionProperty resetScoreAction;
        public float initTime = 120f;
        public TextMeshPro scoreBoardText;

        public AudioSource audioSourceBGM;
        public AudioSource audioSourceHighScore;
        public AudioSource audioSourceGameOver;

        [HideInInspector]
        public int score = 0;
        [HideInInspector]
        public int highScore = 0;

        private float timeRemaining = 0f;
        private bool isScoring = false;

        new void Start()
        {
            base.Start();
            resetScoreAction.action.Enable();
            if (scoreBoardText)
            {
                scoreBoardText.text = "Hold A to start scoring";
            }
            if (audioSourceBGM)
            {
                audioSourceBGM.Stop();
            }
            if (audioSourceHighScore)
            {
                audioSourceHighScore.Stop();
            }
            if (audioSourceGameOver)
            {
                audioSourceGameOver.Stop();
            }
        }

        public void ChangeScore(int delta)
        {
            if (isScoring)
            {
                score += delta;
            }
        }

        void Update()
        {
            if (resetScoreAction.action.WasPerformedThisFrame())
            {
                ResetScore();
            }
            if (isScoring)
            {
                Scoring();
            }
        }

        private void Scoring()
        {
            timeRemaining -= Time.deltaTime;
            refreshScoreBoard();
            if(timeRemaining <= 0f)
            {
                StopScoring();
            }
        }

        private void ResetScore()
        {
            timeRemaining = initTime;
            score = 0;
            isScoring = true;
            if (gunFrame.GetComponent<VacuumGunAmmoFunction>())
            {
                gunFrame.GetComponent<VacuumGunAmmoFunction>().ResetAmmo();
            }
            refreshScoreBoard();
            if (audioSourceBGM)
            {
                audioSourceBGM.Play();
                audioSourceBGM.pitch = 1;
            }
        }


        void refreshScoreBoard()
        {
            if (scoreBoardText)
            {
                scoreBoardText.text = $"High Score: {highScore}\nScore: {score}\nTime: {timeRemaining.ToString("0.00")}";
                scoreBoardText.color = score > highScore ? Color.yellow : Color.white;
            }
        }

        private void StopScoring()
        {
            isScoring = false;
            if (audioSourceBGM)
            {
                audioSourceBGM.Stop();
            }
            // High score
            if (score > highScore)
            {
                highScore = score;
                if (scoreBoardText)
                {
                    scoreBoardText.text = $"You beat High Score!\nYour Score: {highScore}\nHold A to restart";
                    scoreBoardText.color = Color.yellow;
                }
                if (audioSourceHighScore)
                {
                    audioSourceHighScore.Play();
                }
            }
            // Game over
            else
            {
                if (scoreBoardText)
                {
                    scoreBoardText.text = $"High Score: {highScore}\nYour Score: {score}\nHold A to restart";
                    scoreBoardText.color = Color.white;
                }
                if (audioSourceGameOver)
                {
                    audioSourceGameOver.Play();
                }
            }
        }
    }
}