using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    public Dictionary<e_Keys, Key> m_Keys = new Dictionary<e_Keys, Key>(3);
    public Dictionary<e_Keys, Key> m_AllKeys = new Dictionary<e_Keys, Key>(3);

    public Key m_KeyCaptain;     // Gold Key 
    public Key m_KeyControlRoom; // Green Key
    public Key m_KeyPilot;       // Silver Key

    void Start()
    {
        m_AllKeys.Add(e_Keys.CaptainKey, m_KeyCaptain);
        m_AllKeys.Add(e_Keys.ControlKey, m_KeyControlRoom);
        m_AllKeys.Add(e_Keys.PilotKey, m_KeyPilot);

       
        Debug.Log("ALL KEYS ADDED");
        

    }
}
