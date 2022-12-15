using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

public class OpenWeatherRequester : MonoBehaviour
{
    string _apiKey = "4c34dd9700cc757fc47a9d7df35c44d7";
    string pathToSite = "api.openweathermap.org/data/2.5/weather?";
    //WeatherInfo weatherInfo;
    [SerializeField] WeatherVisualizer weatherVisualizer;
    [SerializeField] ForecastVisualizer forecastVisualizer;

    [SerializeField] GameObject errorMessage;
    //dynamic _parsedResult;
    public void CheckWeather(string city)
    {
        StartCoroutine(SendRequest(city));
        StartCoroutine(CheckForeCast(city));
    }
    IEnumerator SendRequest(string city)
    {
        string _uri= pathToSite+ "q=" + city + "&appid=" + _apiKey;
        //Debug.Log("Start");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(_uri);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                    errorMessage.SetActive(true);
                if (PlayerPrefs.HasKey(city))
                {
                    WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(PlayerPrefs.GetString(city));
                    weatherVisualizer.UpdateInfo(weatherInfo);
                }
            }
            else
            {
                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(unityWebRequest.downloadHandler.text);
                PlayerPrefs.SetString(city, unityWebRequest.downloadHandler.text);
                weatherVisualizer.UpdateInfo(weatherInfo);
            }
        }
    }
    //public WeatherInfo GetWeatherInfo() => weatherInfo;
    IEnumerator CheckForeCast(string city)
    {
        string _uri = "api.openweathermap.org/data/2.5/forecast?q=" + city + "&appid=" + _apiKey;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(_uri);
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                errorMessage.SetActive(true);
                if (PlayerPrefs.HasKey(city + "Forecast"))
                {
                    ForecastInfo forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(PlayerPrefs.GetString(city + "Forecast"));
                    forecastVisualizer.UpdateForecast(forecastInfo);
                }
            }
            else
            {
                ForecastInfo forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(unityWebRequest.downloadHandler.text);
                forecastVisualizer.UpdateForecast(forecastInfo);
                PlayerPrefs.SetString(city + "Forecast", unityWebRequest.downloadHandler.text);
            }

        }
    }
}
