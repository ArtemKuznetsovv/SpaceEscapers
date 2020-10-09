using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameLogic : MonoBehaviour
{

    [SerializeField] private GameObject WinningText;
    [SerializeField] private Text WinningTimer;
    [SerializeField] private GameObject LosingText;
    bool DisableAll = false;
    public OxygenBar timer;
    [SerializeField]
    private AudioManager m_AudioManager;

    public void LoseGame()
    {
        if (!DisableAll)
        {
            DisableAll = true;
            m_AudioManager.Play("LooseTheme");
            Debug.Log("Game Lost");
            LosingText.SetActive(true);

            StartCoroutine(ChangeToMainMenu());
        }
    }

    public void WinGame()
    {
        if (!DisableAll)
        {
            try
            {
                DisableAll = true;
                Debug.Log("Game Won");
                m_AudioManager.Play("WinTheme");
                TimeSpan time = TimeSpan.FromSeconds(timer.Timer);
                WinningTimer.text = time.ToString(@"mm\:ss");
                WinningText.SetActive(true);

                CheckHighScore(time);
                StartCoroutine(ChangeToMainMenu());
            }
            catch (Exception exp)
            {

            }
        }
    }

    private void CheckHighScore(TimeSpan time)
    {
        int i = 1;
        TimeSpan score;
        while (i <= 5)
        {
            score = TimeSpan.FromSeconds((double)PlayerPrefs.GetFloat("Score" + i, 0f));
            if ((score <= TimeSpan.Zero) || (score > TimeSpan.Zero) && (time < score))
            {
                updateHighscore(time, i);
                return;
            }
            i++;
        }
        /*
        TimeSpan score1 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Score1", float.MaxValue));
        if (time < score1)
        {
            updateHighscore(time, 1);
        }
        else
        {
            TimeSpan score2 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Score2", float.MaxValue));
            if (time <score2)
            {
                updateHighscore(time, 2);
            }
            else
            {
                TimeSpan score3 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Score3", float.MaxValue));
                if (time < score3)
                {
                    updateHighscore(time, 3);
                }
                else
                {
                    TimeSpan score4 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Score4", float.MaxValue));
                    if (time < score4)
                    {
                        updateHighscore(time, 4);
                    }
                    else
                    {
                        TimeSpan score5 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Score5", float.MaxValue));
                        if (time < score5)
                        {
                            updateHighscore(time, 5);
                        }
                    }
                }
            }
        }
        */
    }

    private void updateHighscore(TimeSpan newTime, int position)
    {
        int i = 5;
        while ( i > position)
        {
            PlayerPrefs.SetFloat("Score" + i, PlayerPrefs.GetFloat("Score" + (i - 1)));
            i--;
        }
        PlayerPrefs.SetFloat("Score" + i, (float)newTime.TotalSeconds);
    }

    private IEnumerator ChangeToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

}
