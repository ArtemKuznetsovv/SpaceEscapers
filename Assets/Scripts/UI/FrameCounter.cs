using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour
{
    private Text m_FrameText;
    private int m_Framescounter;
    private float m_TimesSume =0 ;
    // Start is called before the first frame update
    void Start()
    {
        m_FrameText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TimesSume += Time.deltaTime;
        m_Framescounter++;
        if(m_Framescounter > 7)
        {
            m_FrameText.text = "FPS" + 1 / (m_TimesSume / m_Framescounter);
            m_TimesSume = 0;
            m_Framescounter = 0;
        }


    }
}
