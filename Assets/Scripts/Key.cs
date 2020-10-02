using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_Keys
{
    CaptainKey,
    ControlKey,
    PilotKey
}

public class Key : MonoBehaviour
{
    public e_Keys Type;
    public KeyUI key_UI;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, Time.deltaTime * 30, 0));
    }
}
