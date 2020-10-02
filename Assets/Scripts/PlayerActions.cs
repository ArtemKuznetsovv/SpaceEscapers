using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Camera MainCamera;
    public float MaxDistance = 3f;

    [SerializeField]
    private PlayerInventory m_PlayerInventory;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerEventManager.isKeyDown())
        {
           handleRaycast();
        }
    }

    private void handleRaycast()
    {
        Ray ray;
        RaycastHit hit;

        ray = MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            ActionHit(hit);
        }
    }

    private void ActionHit(RaycastHit i_Hit)
    {
        Debug.Log("Hit Detected");
        switch (i_Hit.transform.tag)
        {
            case nameof(PlayerEventManager.e_ActiveItems.RegularGlassDoor):
                OpenRegularGlassDoor(i_Hit.transform.parent);
                break;
            case nameof(PlayerEventManager.e_ActiveItems.OneSidedGlassDoor):
                OpenOneSidedGlassDoor(i_Hit.transform.parent);
                break;
            case nameof(PlayerEventManager.e_ActiveItems.CaptinRoomDoor):
                OpenCaptainRoomDoor(i_Hit.transform.parent);
                break;
            case nameof(PlayerEventManager.e_ActiveItems.ControlRoomDoor):
                OpenControlRoomDoor(i_Hit.transform.parent);
                break;
            case nameof(PlayerEventManager.e_ActiveItems.Key):
                ObtainKey(i_Hit.transform);
                break;

        }
    }

    #region All Doors

    private void OpenRegularGlassDoor(Transform i_Door)
    {
        Debug.Log("Open Reular GlassDoor");
        Animator animator = i_Door.GetComponent<Animator>();
        animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
    }

    private void OpenOneSidedGlassDoor(Transform i_Door)
    {
        if (i_Door.position.x < MainCamera.transform.position.x)
        {
            Debug.Log("Open OneSided GlassDoor");
            Animator animator = i_Door.GetComponent<Animator>();
            animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
        }
    }

    private void OpenCaptainRoomDoor(Transform i_Door)
    {
        if (m_PlayerInventory.m_Keys.ContainsKey(e_Keys.CaptainKey))
        {
            Animator animator = i_Door.GetComponent<Animator>();
            animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
        }
        else
        {
            Debug.Log("No Key Found");
            Debug.Log( m_PlayerInventory.m_AllKeys[e_Keys.CaptainKey]);
            missingKeyAnimation(e_Keys.CaptainKey);
        }
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1);
    }

    private void missingKeyAnimation(e_Keys i_AnimateKey)
    {
        m_PlayerInventory.m_AllKeys[i_AnimateKey].key_UI.GetComponent<Animator>().SetTrigger("Blink");
    }

    private void OpenControlRoomDoor(Transform i_Door)
    {
        bool keysMissing = false;

        foreach (e_Keys key in Enum.GetValues(typeof(e_Keys)))
        {
            if (!m_PlayerInventory.m_Keys.ContainsKey(key))
            {

                Debug.Log("Keys Missing");
                keysMissing = true;
                missingKeyAnimation(key);
            }
        }
        if (!keysMissing)
        {
            Animator animator = i_Door.GetComponent<Animator>();
            animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
        }
    }

    #endregion

    private void ObtainKey(Transform i_Key) 
    {
        Key key = i_Key.GetComponent<Key>();
        key.key_UI.GetComponent<Animator>().enabled = false;
        key.key_UI.Show(true);
        m_PlayerInventory.m_Keys.Add(key.Type, key);
        i_Key.gameObject.SetActive(false);
        Debug.Log("Obtained Key: " + key.Type);
    }
}
