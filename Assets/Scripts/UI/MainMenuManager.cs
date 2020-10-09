using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject Canvas_Highscore;
    public SimpleHelvetica HighScoreText;
    public GameObject Canvas_HowToPlay;

    [SerializeField]
    private AudioSource m_StartGameAduio;
    [SerializeField]
    private AudioSource m_HowToPlayAduio;
    [SerializeField]
    private AudioSource m_HighScoreAduio;
    [SerializeField]
    private AudioSource m_QuitAduio;

    

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit))
        {

            Debug.Log("Hit");
            try
            {
                hit.transform.GetComponent<MainMenuLightItem>().Focus();
            }
            catch(Exception e)
            {

            }

            if(Input.anyKeyDown && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl))
            {
                switch(hit.transform.tag)
                {
                    case "MainMenu_StartGame":
                        m_StartGameAduio.PlayOneShot(m_StartGameAduio.clip);
                        SceneManager.LoadScene(1);
                        break;
                    case "MainMenu_Highscores":
                        m_HighScoreAduio.PlayOneShot(m_HighScoreAduio.clip);
                        Canvas_Highscore.gameObject.SetActive(true);
                        Debug.Log("Highscores");
                        RetriveHighscore();
                        break;
                    case "MainMenu_HowToPlay":
                        m_HowToPlayAduio.PlayOneShot(m_HowToPlayAduio.clip);
                        Canvas_HowToPlay.gameObject.SetActive(true);
                        Debug.Log("How to Play");
                        break;
                    case "MainMenu_Quit":
                        m_QuitAduio.PlayOneShot(m_QuitAduio.clip);
                        Debug.Log("Quit");
                        Application.Quit();
                        break;
                    case "InformationPlank":
                        hit.transform.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }


    private void RetriveHighscore()
    {
        try
        {
            HighScoreText.Text = String.Format(
                "1.   {0}{5}" +
                "2.   {1}{5}" +
                "3.   {2}{5}" +
                "4.   {3}{5}" +
                "5.   {4}{5}",
                retriveScore(1), retriveScore(2), retriveScore(3), retriveScore(4), retriveScore(5),
                "\n");
            HighScoreText.ApplyMeshRenderer();
            HighScoreText.GenerateText();
        }
        catch (Exception exp)
        {

        }
    }

    private string retriveScore(int i)
    {
        float score = PlayerPrefs.GetFloat("Score" + i, float.MaxValue);
        if (score == float.MaxValue)
        {
            return "---";
        }
        else
        {
            TimeSpan time = TimeSpan.FromSeconds(score);
            return time.ToString(@"mm\:ss");
        }
    }
}
