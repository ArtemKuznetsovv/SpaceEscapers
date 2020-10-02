using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Camera MainCamera;
    public float MovmentSpeed = 2.5f;
    public Transform Indicator_ZY;
    public Transform Indicator_XY;
    private Rigidbody m_RigidBody;

    void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerEventManager.isClicked())
        {
            //this.transform.position += MainCamera.transform.forward * MovmentSpeed * Time.deltaTime;
            m_RigidBody.AddForce(MainCamera.transform.forward * MovmentSpeed*Time.deltaTime,ForceMode.Impulse);
           // m_RigidBody.velocity = MainCamera.transform.forward * MovmentSpeed;
           Indicator_ZY.position = new Vector3(40f,
                                                this.transform.position.y,
                                                this.transform.position.z);
            Indicator_XY.position = new Vector3(this.transform.position.x,
                                                this.transform.position.y,
                                                -40f);
        }
    }
}
