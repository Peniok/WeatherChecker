using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForecastElement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI temperatureText, timeText;
    
    public void SetInfo(Sprite sprite, int temperature, string time, Transform parent)
    {
        icon.sprite = sprite;
        temperatureText.text = temperature + "<sprite index=0>";
        timeText.text = time;
    }
}
