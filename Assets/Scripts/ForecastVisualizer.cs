using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ForecastVisualizer : MonoBehaviour
{
    [SerializeField] Image[] icons;
    [SerializeField] TextMeshProUGUI[] temperatures, times;
    [SerializeField] Sprite[] weatherIcons;

    public void UpdateForecast(ForecastInfo forecastInfo)
    {
        for (int i = 0; i < 3; i++)
        {
            SetSprite(forecastInfo,icons[i]);
            temperatures[i].text = (int)forecastInfo.list[i].main.temp + "<sprite index=0>";
            times[i].text = UnixTimeToDateTime(forecastInfo.list[i].dt)+"";
        }
    }
    public void SetSprite(ForecastInfo weatherInfo, Image weatherImage)
    {
        int weatherid = weatherInfo.list[0].weather[0].id;
        //Debug.Log(weatherid);
        if (weatherid < 300)
        {
            weatherImage.sprite = weatherIcons[0];
        }
        else if (weatherid < 400)
        {
            weatherImage.sprite = weatherIcons[1];
        }
        else if (weatherid < 600)
        {
            weatherImage.sprite = weatherIcons[2];
        }
        else if (weatherid < 700)
        {
            weatherImage.sprite = weatherIcons[3];
        }
        else if (weatherid < 800)
        {
            weatherImage.sprite = weatherIcons[4];
        }
        else if (weatherid == 800)
        {
            weatherImage.sprite = weatherIcons[5];
        }
        else if (weatherid == 801)
        {
            weatherImage.sprite = weatherIcons[6];
        }
        else if (weatherid == 802)
        {
            weatherImage.sprite = weatherIcons[7];
        }
        else
        {
            weatherImage.sprite = weatherIcons[8];
        }
    }
    public DateTime UnixTimeToDateTime(double UnixTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
        return origin.AddSeconds(UnixTime);
    }
}
