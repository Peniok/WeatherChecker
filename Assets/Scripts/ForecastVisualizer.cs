using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ForecastVisualizer : MonoBehaviour
{
    //[SerializeField] Image[] icons;
    //[SerializeField] TextMeshProUGUI[] temperatures, times;
    List<ForecastElement> forecastElements;
    [SerializeField] Sprite[] weatherIcons;
    [SerializeField] GameObject forecastElementPrefab;
    [SerializeField] Transform parentForForecast;
    [SerializeField] ScrollRect scrollRect;
    private void Awake()
    {
        forecastElements = new List<ForecastElement>();
    }
    public void UpdateForecast(ForecastInfo forecastInfo)
    {
        scrollRect.normalizedPosition = new Vector2(0,1);
        for (int i = 0; i < forecastInfo.list.Length; i++)
        {
            ForecastElement forecastElement;
            if (i >= forecastElements.Count)
            {
                forecastElement = Instantiate(forecastElementPrefab, parentForForecast).GetComponent<ForecastElement>();
                forecastElements.Add(forecastElement);
            }
            else
            {
                forecastElement = forecastElements[i];
            }
            forecastElement.SetInfo(GetSprite(forecastInfo.list[i]), (int)forecastInfo.list[i].main.temp, UnixTimeToDateTime(forecastInfo.list[i].dt) + "",parentForForecast);
        }
        //for (int i = 0; i < 3; i++)
        //{
        //    SetSprite(forecastInfo,icons[i]);
        //    temperatures[i].text = (int)forecastInfo.list[i].main.temp + "<sprite index=0>";
        //    times[i].text = UnixTimeToDateTime(forecastInfo.list[i].dt)+"";
        //}
    }
    public Sprite GetSprite(InfoForForecast infoForForecast)
    {
        int weatherid = infoForForecast.weather[0].id;
        //Debug.Log(weatherid);
        if (weatherid < 300)
        {
            return weatherIcons[0];
        }
        else if (weatherid < 400)
        {
            return weatherIcons[1];
        }
        else if (weatherid < 600)
        {
            return weatherIcons[2];
        }
        else if (weatherid < 700)
        {
            return weatherIcons[3];
        }
        else if (weatherid < 800)
        {
            return weatherIcons[4];
        }
        else if (weatherid == 800)
        {
            return weatherIcons[5];
        }
        else if (weatherid == 801)
        {
            return weatherIcons[6];
        }
        else if (weatherid == 802)
        {
            return weatherIcons[7];
        }
        else
        {
            return weatherIcons[8];
        }
    }
    public DateTime UnixTimeToDateTime(double UnixTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
        return origin.AddSeconds(UnixTime);
    }
}
