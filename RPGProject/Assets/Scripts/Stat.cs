using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public float MaxValue { get; set; }

    private float currentValue;
    public float CurrentValue {
        get {
            return currentValue;
        }
        set {
            if (value > MaxValue) currentValue = MaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;
        }
    }

    private Image statbar;
    private Image statbarBG;
    private Text statbarText;

    [Header("Settings")]
    public float lerpTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        statbar = GetComponent<Image>();
        statbarBG = transform.GetChild(0).GetComponent<Image>();
        statbarText = transform.GetChild(1).GetComponent<Text>();

        //Convert lerpTime into delta time value
        lerpTime = lerpTime * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        statbar.fillAmount = Mathf.Lerp(statbar.fillAmount, (currentValue / MaxValue), lerpTime);
        statbarBG.color = colorLerp(25);
        statbarText.text = CurrentValue + "/" + MaxValue;
    }

    public void Initialize(float maxValue, float currentValue) {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }

    //Lerps the colourValue 
    private Color colorLerp(int minValue) {
        return new Color(
            Mathf.Lerp(statbarBG.color.r, colourValue(minValue), lerpTime),
            Mathf.Lerp(statbarBG.color.b, colourValue(minValue), lerpTime),
            Mathf.Lerp(statbarBG.color.g, colourValue(minValue), lerpTime));
    }

    //Calculates RBG values for statbar BG's based on their value 
    // Value ranges between given minValue and 1.0 (currentValue == MaxValue)
    private float colourValue(int minValue) {
        return (minValue + (currentValue / MaxValue) * (100 - minValue)) / 100;
    }

   
}
