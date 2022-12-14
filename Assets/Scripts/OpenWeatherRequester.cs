using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class OpenWeatherRequester : MonoBehaviour
{
    string _apiKey = "4c34dd9700cc757fc47a9d7df35c44d7";
    string pathToSite = "api.openweathermap.org/data/2.5/weather?";
    //WeatherInfo weatherInfo;
    [SerializeField] WeatherVisualizer weatherVisualizer;
    [SerializeField] GameObject errorMessage;
    //dynamic _parsedResult;
    public void CheckWeather(string city)
    {
        StartCoroutine(SendRequest(city));
    }
    IEnumerator SendRequest(string city)
    {
        string _uri= pathToSite+ "q=" + city + "&appid=" + _apiKey;
        Debug.Log("Start");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(_uri);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                    errorMessage.SetActive(true);
                if (PlayerPrefs.HasKey(city))
                {
                    Debug.Log("ShowingOldResults");
                    WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(PlayerPrefs.GetString(city));
                    weatherVisualizer.UpdateInfo(weatherInfo);
                }
            }
            else
            {
                Debug.Log("ShowingNewResults");
                string _result;

                _result = unityWebRequest.downloadHandler.text;

                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(_result);
                PlayerPrefs.SetString(city, unityWebRequest.downloadHandler.text);
                weatherVisualizer.UpdateInfo(weatherInfo);
            }
            Debug.Log("End");

        }
    }
    //public WeatherInfo GetWeatherInfo() => weatherInfo;
}
