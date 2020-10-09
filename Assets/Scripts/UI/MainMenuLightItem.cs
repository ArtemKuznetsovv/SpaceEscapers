using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLightItem : MonoBehaviour
{
    //private int framesToMove = 120;
    public int NumberofLights;
    public Light[] lights;
    private Vector3[] sourcePositions;
    private Vector3[] targetPositions;
    private bool isinitial = false;

    void Start()
    {
        sourcePositions = new Vector3[NumberofLights];
        targetPositions = new Vector3[NumberofLights];

        for (int i = 0; i < NumberofLights; i++)
        {
            sourcePositions[i] = lights[i].transform.position;
            targetPositions[i] = lights[i].transform.position - (Vector3.forward * 12) + (Vector3.up * 3);
        }
        Debug.Log(lights.Length + "\n" + sourcePositions.Length + "\n" + targetPositions.Length);
    }

    public void Focus()
    {
        Debug.Log("FOCUS");
        for (int i = 0; i < NumberofLights; i++)
        {
            lights[i].intensity = 40;
            lights[i].range = 20;
            lights[i].transform.position = Vector3.MoveTowards(lights[i].transform.position,
                targetPositions[i], 2f * Time.deltaTime);
        }
    }
}
