using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer m_KeyRendrer;

    void Start()
    {
        //Show(false);
    }

    public void Show(bool i_ToShow)
    {

        m_KeyRendrer.enabled = i_ToShow;
        Debug.Log(m_KeyRendrer.enabled);
    }

}
