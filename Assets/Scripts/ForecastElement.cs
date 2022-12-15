using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForecastElement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI temperatureText, timeText;
    
    public void SetInfo(Sprite sprite, int temperature, string time)
    {
        icon.sprite = sprite;
        temperatureText.text = temperature.ToString();
        timeText.text = time;
    }
}
