using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OxygenBar : MonoBehaviour
{
    public Image Bar;
    public Text Precentage;
    private BarStatus status = BarStatus.White;

    public static double MaxLength = 450;
    public double CurrentTime = MaxLength;

    private readonly Color white = new Color(255, 255, 255, 125);

    private enum BarStatus
    {
        White,
        Orange,
        Red
    }

    // Start is called before the first frame update
    void Start()
    {
        status = BarStatus.White;
        Bar.color = white;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            Bar.fillAmount = (float)(CurrentTime / MaxLength);
            Precentage.text = (int)(CurrentTime*100 / MaxLength) + "%";
            UpdateColor();
        }
        else
        {
            // TODO: Game Over!
        }
    }

    private void UpdateColor()
    {
        if (status != BarStatus.Orange &&
            Bar.fillAmount < 0.5 &&
            Bar.fillAmount > 0.2)
        {
            Bar.color = new Color(252, 88, 0, 180);
            Precentage.color = new Color(252, 88, 0, 180);
            status = BarStatus.Orange;
            return;
        }

        if (status != BarStatus.Red &&
            Bar.fillAmount < 0.2)
        {
            Bar.color = new Color(255, 0, 0, 200);
            Precentage.color = new Color(255, 0, 0, 200);
            Precentage.fontStyle = FontStyle.Bold;
            status = BarStatus.Red;
            return;
        }
    }
}
