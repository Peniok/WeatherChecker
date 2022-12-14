using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeatherVisualizer : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField]TMP_InputField inputField;
    [SerializeField] OpenWeatherRequester weatherRequester;
    [SerializeField] Image weatherImage;
    [SerializeField] Sprite[] weatherIcons;
    [SerializeField] TextMeshProUGUI temperatureText, temperatureMinText, temperatureMaxText;
    [SerializeField] TextMeshProUGUI descriptionText, timeOfCheckingText;
    [SerializeField] GameObject weatherInfoGameObject;
    private void Start()
    {
        button.onClick.AddListener(CallSearch);
    }
    public void CallSearch()
    {
        weatherRequester.CheckWeather(inputField.text);
    }
    public void UpdateInfo(WeatherInfo weatherInfo)
    {
        weatherInfoGameObject.SetActive(true);
        int weatherid = weatherInfo.weather[0].id;
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
        temperatureText.text = (int)weatherInfo.main.temp + "<sprite index=0>";
        temperatureMinText.text = "Min "+(int)weatherInfo.main.tempMin + "<sprite index=0>";
        temperatureMaxText.text = "Max " + (int)weatherInfo.main.tempMax + "<sprite index=0>";

        timeOfCheckingText.text = "Дата оновлення даних " + Convert.ToString(UnixTimeToDateTime(Convert.ToDouble(weatherInfo.dt))); 

        descriptionText.text = weatherInfo.weather[0].description;
    }
    public DateTime UnixTimeToDateTime(double UnixTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
        return origin.AddSeconds(UnixTime);
    }
}
