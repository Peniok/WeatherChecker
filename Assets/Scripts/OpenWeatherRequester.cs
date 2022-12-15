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
                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(unityWebRequest.downloadHandler.text);
                PlayerPrefs.SetString(city, unityWebRequest.downloadHandler.text);
                weatherVisualizer.UpdateInfo(weatherInfo);
            }
            Debug.Log("End");

        }
    }
    //public WeatherInfo GetWeatherInfo() => weatherInfo;
    IEnumerator CheckForeCast(string city)
    {
        string _uri = "api.openweathermap.org/data/2.5/forecast?q=" + city + "&appid=" + _apiKey;

        Debug.Log("Start");
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



            //Debug.Log(weatherInfo.list[0].main.temp);
            //Debug.Log(weatherInfo.list[0].weather[0].id);
            //Debug.Log(UnixTimeToDateTime(weatherInfo.list[0].dt));

            //Debug.Log(weatherInfo.list[1].main.temp);
            //Debug.Log(weatherInfo.list[1].weather[0].id);
            //Debug.Log(UnixTimeToDateTime(weatherInfo.list[1].dt));
            //Debug.Log(weatherInfo.list[2].main.temp);
            //Debug.Log(weatherInfo.list[2].weather[0].id);
            //Debug.Log(UnixTimeToDateTime(weatherInfo.list[2].dt));
        }
        #region i
        //        { "cod":"200","message":0,"cnt":40,
        //                "list":[
        //                { "dt":1671051600,
        //                    "main":{ "temp":269.11,"feels_like":264.14,"temp_min":268.96,"temp_max":269.11,"pressure":1019,"sea_level":1019,"grnd_level":1003,"humidity":91,"temp_kf":0.15},
        //                        "weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],
        //                            "clouds":{ "all":100},"wind":{ "speed":3.71,"deg":138,"gust":9.9},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-14 21:00:00"},
        //pu

        //{ "dt":1671062400,"main":{ "temp":269.45,"feels_like":263.18,"temp_min":269.45,"temp_max":270.14,"pressure":1019,"sea_level":1019,"grnd_level":1001,"humidity":86,"temp_kf":-0.69},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":100},"wind":{ "speed":5.69,"deg":137,"gust":13.54},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-15 00:00:00"},{ "dt":1671073200,"main":{ "temp":270,"feels_like":263,"temp_min":270,"temp_max":270.44,"pressure":1016,"sea_level":1016,"grnd_level":999,"humidity":85,"temp_kf":-0.44},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":7.56,"deg":146,"gust":16.71},"visibility":2970,"pop":0.37,"snow":{ "3h":0.16},"sys":{ "pod":"n"},"dt_txt":"2022-12-15 03:00:00"},{ "dt":1671084000,"main":{ "temp":271.13,"feels_like":264.2,"temp_min":271.13,"temp_max":271.13,"pressure":1013,"sea_level":1013,"grnd_level":997,"humidity":92,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":7.99,"deg":156,"gust":19.05},"visibility":1440,"pop":0.62,"snow":{ "3h":0.74},"sys":{ "pod":"d"},"dt_txt":"2022-12-15 06:00:00"},{ "dt":1671094800,"main":{ "temp":273.2,"feels_like":266.84,"temp_min":273.2,"temp_max":273.2,"pressure":1013,"sea_level":1013,"grnd_level":996,"humidity":87,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":8.14,"deg":170,"gust":18.35},"visibility":10000,"pop":0.35,"snow":{ "3h":0.13},"sys":{ "pod":"d"},"dt_txt":"2022-12-15 09:00:00"},{ "dt":1671105600,"main":{ "temp":273.23,"feels_like":268.09,"temp_min":273.23,"temp_max":273.23,"pressure":1013,"sea_level":1013,"grnd_level":997,"humidity":98,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":5.44,"deg":183,"gust":14.56},"visibility":105,"pop":0.66,"snow":{ "3h":1.32},"sys":{ "pod":"d"},"dt_txt":"2022-12-15 12:00:00"},{ "dt":1671116400,"main":{ "temp":273.61,"feels_like":269.39,"temp_min":273.61,"temp_max":273.61,"pressure":1014,"sea_level":1014,"grnd_level":998,"humidity":98,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":4.06,"deg":190,"gust":11.09},"visibility":63,"pop":0.78,"snow":{ "3h":0.74},"sys":{ "pod":"n"},"dt_txt":"2022-12-15 15:00:00"},{ "dt":1671127200,"main":{ "temp":273.81,"feels_like":270.26,"temp_min":273.81,"temp_max":273.81,"pressure":1015,"sea_level":1015,"grnd_level":999,"humidity":97,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":100},"wind":{ "speed":3.22,"deg":200,"gust":7.8},"visibility":10000,"pop":0.5,"sys":{ "pod":"n"},"dt_txt":"2022-12-15 18:00:00"},{ "dt":1671138000,"main":{ "temp":273.72,"feels_like":271.24,"temp_min":273.72,"temp_max":273.72,"pressure":1017,"sea_level":1017,"grnd_level":1001,"humidity":97,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":100},"wind":{ "speed":2.1,"deg":230,"gust":5.44},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-15 21:00:00"},{ "dt":1671148800,"main":{ "temp":272.1,"feels_like":272.1,"temp_min":272.1,"temp_max":272.1,"pressure":1019,"sea_level":1019,"grnd_level":1002,"humidity":98,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":99},"wind":{ "speed":0.9,"deg":310,"gust":1.99},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-16 00:00:00"},{ "dt":1671159600,"main":{ "temp":271.27,"feels_like":271.27,"temp_min":271.27,"temp_max":271.27,"pressure":1019,"sea_level":1019,"grnd_level":1002,"humidity":95,"temp_kf":0},"weather":[{ "id":803,"main":"Clouds","description":"broken clouds","icon":"04n"}],"clouds":{ "all":68},"wind":{ "speed":1.16,"deg":53,"gust":1.52},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-16 03:00:00"},{ "dt":1671170400,"main":{ "temp":272.47,"feels_like":269,"temp_min":272.47,"temp_max":272.47,"pressure":1019,"sea_level":1019,"grnd_level":1003,"humidity":94,"temp_kf":0},"weather":[{ "id":803,"main":"Clouds","description":"broken clouds","icon":"04d"}],"clouds":{ "all":84},"wind":{ "speed":2.82,"deg":101,"gust":6.08},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-16 06:00:00"},{ "dt":1671181200,"main":{ "temp":274.04,"feels_like":269.93,"temp_min":274.04,"temp_max":274.04,"pressure":1019,"sea_level":1019,"grnd_level":1002,"humidity":98,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}],"clouds":{ "all":100},"wind":{ "speed":4.05,"deg":129,"gust":10.71},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-16 09:00:00"},{ "dt":1671192000,"main":{ "temp":274.84,"feels_like":270.12,"temp_min":274.84,"temp_max":274.84,"pressure":1016,"sea_level":1016,"grnd_level":1000,"humidity":98,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}],"clouds":{ "all":100},"wind":{ "speed":5.44,"deg":137,"gust":13.89},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-16 12:00:00"},{ "dt":1671202800,"main":{ "temp":275.02,"feels_like":269.77,"temp_min":275.02,"temp_max":275.02,"pressure":1013,"sea_level":1013,"grnd_level":997,"humidity":97,"temp_kf":0},"weather":[{ "id":500,"main":"Rain","description":"light rain","icon":"10n"}],"clouds":{ "all":100},"wind":{ "speed":6.68,"deg":135,"gust":16.5},"visibility":10000,"pop":0.85,"rain":{ "3h":0.52},"sys":{ "pod":"n"},"dt_txt":"2022-12-16 15:00:00"},{ "dt":1671213600,"main":{ "temp":275.75,"feels_like":270.67,"temp_min":275.75,"temp_max":275.75,"pressure":1010,"sea_level":1010,"grnd_level":994,"humidity":97,"temp_kf":0},"weather":[{ "id":501,"main":"Rain","description":"moderate rain","icon":"10n"}],"clouds":{ "all":100},"wind":{ "speed":6.77,"deg":153,"gust":16.56},"visibility":2776,"pop":1,"rain":{ "3h":5.02},"sys":{ "pod":"n"},"dt_txt":"2022-12-16 18:00:00"},{ "dt":1671224400,"main":{ "temp":276.58,"feels_like":272.56,"temp_min":276.58,"temp_max":276.58,"pressure":1011,"sea_level":1011,"grnd_level":995,"humidity":98,"temp_kf":0},"weather":[{ "id":500,"main":"Rain","description":"light rain","icon":"10n"}],"clouds":{ "all":100},"wind":{ "speed":4.93,"deg":189,"gust":13.96},"visibility":124,"pop":0.94,"rain":{ "3h":1.57},"sys":{ "pod":"n"},"dt_txt":"2022-12-16 21:00:00"},{ "dt":1671235200,"main":{ "temp":276.24,"feels_like":272.76,"temp_min":276.24,"temp_max":276.24,"pressure":1012,"sea_level":1012,"grnd_level":996,"humidity":99,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":100},"wind":{ "speed":3.85,"deg":204,"gust":9.97},"visibility":107,"pop":0.79,"sys":{ "pod":"n"},"dt_txt":"2022-12-17 00:00:00"},{ "dt":1671246000,"main":{ "temp":275.51,"feels_like":272.6,"temp_min":275.51,"temp_max":275.51,"pressure":1013,"sea_level":1013,"grnd_level":997,"humidity":99,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04n"}],"clouds":{ "all":100},"wind":{ "speed":2.86,"deg":224,"gust":7.56},"visibility":8860,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-17 03:00:00"},{ "dt":1671256800,"main":{ "temp":274.97,"feels_like":272.26,"temp_min":274.97,"temp_max":274.97,"pressure":1015,"sea_level":1015,"grnd_level":999,"humidity":97,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}],"clouds":{ "all":100},"wind":{ "speed":2.52,"deg":242,"gust":5.24},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-17 06:00:00"},{ "dt":1671267600,"main":{ "temp":274.83,"feels_like":271.9,"temp_min":274.83,"temp_max":274.83,"pressure":1017,"sea_level":1017,"grnd_level":1000,"humidity":98,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}],"clouds":{ "all":100},"wind":{ "speed":2.73,"deg":291,"gust":6.21},"visibility":9698,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-17 09:00:00"},{ "dt":1671278400,"main":{ "temp":274.4,"feels_like":271.79,"temp_min":274.4,"temp_max":274.4,"pressure":1017,"sea_level":1017,"grnd_level":1001,"humidity":96,"temp_kf":0},"weather":[{ "id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}],"clouds":{ "all":100},"wind":{ "speed":2.32,"deg":313,"gust":5.35},"visibility":10000,"pop":0.02,"sys":{ "pod":"d"},"dt_txt":"2022-12-17 12:00:00"},{ "dt":1671289200,"main":{ "temp":273.72,"feels_like":270.2,"temp_min":273.72,"temp_max":273.72,"pressure":1019,"sea_level":1019,"grnd_level":1003,"humidity":95,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":3.16,"deg":320,"gust":6.65},"visibility":10000,"pop":0.78,"snow":{ "3h":0.73},"sys":{ "pod":"n"},"dt_txt":"2022-12-17 15:00:00"},{ "dt":1671300000,"main":{ "temp":273.15,"feels_like":269.34,"temp_min":273.15,"temp_max":273.15,"pressure":1020,"sea_level":1020,"grnd_level":1004,"humidity":97,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":3.37,"deg":332,"gust":6.92},"visibility":221,"pop":1,"snow":{ "3h":0.66},"sys":{ "pod":"n"},"dt_txt":"2022-12-17 18:00:00"},{ "dt":1671310800,"main":{ "temp":272.45,"feels_like":268.09,"temp_min":272.45,"temp_max":272.45,"pressure":1022,"sea_level":1022,"grnd_level":1005,"humidity":95,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":3.89,"deg":333,"gust":7.36},"visibility":443,"pop":0.88,"snow":{ "3h":1.28},"sys":{ "pod":"n"},"dt_txt":"2022-12-17 21:00:00"},{ "dt":1671321600,"main":{ "temp":271.28,"feels_like":266.14,"temp_min":271.28,"temp_max":271.28,"pressure":1024,"sea_level":1024,"grnd_level":1007,"humidity":95,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":4.61,"deg":327,"gust":8.48},"visibility":162,"pop":0.82,"snow":{ "3h":0.66},"sys":{ "pod":"n"},"dt_txt":"2022-12-18 00:00:00"},{ "dt":1671332400,"main":{ "temp":270.54,"feels_like":265.22,"temp_min":270.54,"temp_max":270.54,"pressure":1025,"sea_level":1025,"grnd_level":1008,"humidity":95,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":4.62,"deg":335,"gust":8.71},"visibility":167,"pop":0.86,"snow":{ "3h":1.28},"sys":{ "pod":"n"},"dt_txt":"2022-12-18 03:00:00"},{ "dt":1671343200,"main":{ "temp":270,"feels_like":264.26,"temp_min":270,"temp_max":270,"pressure":1027,"sea_level":1027,"grnd_level":1011,"humidity":95,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":5.06,"deg":332,"gust":9.24},"visibility":152,"pop":1,"snow":{ "3h":1.38},"sys":{ "pod":"d"},"dt_txt":"2022-12-18 06:00:00"},{ "dt":1671354000,"main":{ "temp":270.13,"feels_like":264.22,"temp_min":270.13,"temp_max":270.13,"pressure":1030,"sea_level":1030,"grnd_level":1013,"humidity":94,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":5.38,"deg":342,"gust":9.57},"visibility":186,"pop":1,"snow":{ "3h":1.11},"sys":{ "pod":"d"},"dt_txt":"2022-12-18 09:00:00"},{ "dt":1671364800,"main":{ "temp":270.64,"feels_like":264.79,"temp_min":270.64,"temp_max":270.64,"pressure":1031,"sea_level":1031,"grnd_level":1015,"humidity":93,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13d"}],"clouds":{ "all":100},"wind":{ "speed":5.51,"deg":352,"gust":10.03},"visibility":475,"pop":0.97,"snow":{ "3h":0.6},"sys":{ "pod":"d"},"dt_txt":"2022-12-18 12:00:00"},{ "dt":1671375600,"main":{ "temp":270.66,"feels_like":264.79,"temp_min":270.66,"temp_max":270.66,"pressure":1034,"sea_level":1034,"grnd_level":1017,"humidity":87,"temp_kf":0},"weather":[{ "id":600,"main":"Snow","description":"light snow","icon":"13n"}],"clouds":{ "all":100},"wind":{ "speed":5.55,"deg":359,"gust":11.24},"visibility":10000,"pop":0.2,"snow":{ "3h":0.18},"sys":{ "pod":"n"},"dt_txt":"2022-12-18 15:00:00"},{ "dt":1671386400,"main":{ "temp":269.44,"feels_like":263.65,"temp_min":269.44,"temp_max":269.44,"pressure":1036,"sea_level":1036,"grnd_level":1019,"humidity":82,"temp_kf":0},"weather":[{ "id":803,"main":"Clouds","description":"broken clouds","icon":"04n"}],"clouds":{ "all":82},"wind":{ "speed":4.91,"deg":1,"gust":11.06},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-18 18:00:00"},{ "dt":1671397200,"main":{ "temp":268.42,"feels_like":262.79,"temp_min":268.42,"temp_max":268.42,"pressure":1039,"sea_level":1039,"grnd_level":1022,"humidity":83,"temp_kf":0},"weather":[{ "id":801,"main":"Clouds","description":"few clouds","icon":"02n"}],"clouds":{ "all":13},"wind":{ "speed":4.34,"deg":5,"gust":9.92},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-18 21:00:00"},{ "dt":1671408000,"main":{ "temp":267.42,"feels_like":262.38,"temp_min":267.42,"temp_max":267.42,"pressure":1040,"sea_level":1040,"grnd_level":1023,"humidity":84,"temp_kf":0},"weather":[{ "id":800,"main":"Clear","description":"clear sky","icon":"01n"}],"clouds":{ "all":9},"wind":{ "speed":3.38,"deg":12,"gust":8.47},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-19 00:00:00"},{ "dt":1671418800,"main":{ "temp":266.81,"feels_like":262.41,"temp_min":266.81,"temp_max":266.81,"pressure":1041,"sea_level":1041,"grnd_level":1024,"humidity":85,"temp_kf":0},"weather":[{ "id":800,"main":"Clear","description":"clear sky","icon":"01n"}],"clouds":{ "all":5},"wind":{ "speed":2.67,"deg":14,"gust":7.1},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-19 03:00:00"},{ "dt":1671429600,"main":{ "temp":266,"feels_like":262.33,"temp_min":266,"temp_max":266,"pressure":1042,"sea_level":1042,"grnd_level":1025,"humidity":86,"temp_kf":0},"weather":[{ "id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"clouds":{ "all":5},"wind":{ "speed":2.02,"deg":40,"gust":6.03},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-19 06:00:00"},{ "dt":1671440400,"main":{ "temp":268.39,"feels_like":264.97,"temp_min":268.39,"temp_max":268.39,"pressure":1043,"sea_level":1043,"grnd_level":1026,"humidity":76,"temp_kf":0},"weather":[{ "id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"clouds":{ "all":5},"wind":{ "speed":2.13,"deg":64,"gust":4.33},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-19 09:00:00"},{ "dt":1671451200,"main":{ "temp":269.15,"feels_like":266.61,"temp_min":269.15,"temp_max":269.15,"pressure":1042,"sea_level":1042,"grnd_level":1025,"humidity":79,"temp_kf":0},"weather":[{ "id":800,"main":"Clear","description":"clear sky","icon":"01d"}],"clouds":{ "all":5},"wind":{ "speed":1.63,"deg":78,"gust":3.61},"visibility":10000,"pop":0,"sys":{ "pod":"d"},"dt_txt":"2022-12-19 12:00:00"},{ "dt":1671462000,"main":{ "temp":266.26,"feels_like":266.26,"temp_min":266.26,"temp_max":266.26,"pressure":1042,"sea_level":1042,"grnd_level":1025,"humidity":93,"temp_kf":0},"weather":[{ "id":802,"main":"Clouds","description":"scattered clouds","icon":"03n"}],"clouds":{ "all":46},"wind":{ "speed":1.11,"deg":95,"gust":1.34},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-19 15:00:00"},{ "dt":1671472800,"main":{ "temp":265.53,"feels_like":262.98,"temp_min":265.53,"temp_max":265.53,"pressure":1042,"sea_level":1042,"grnd_level":1025,"humidity":92,"temp_kf":0},"weather":[{ "id":802,"main":"Clouds","description":"scattered clouds","icon":"03n"}],"clouds":{ "all":30},"wind":{ "speed":1.37,"deg":122,"gust":3.51},"visibility":10000,"pop":0,"sys":{ "pod":"n"},"dt_txt":"2022-12-19 18:00:00"}],
        //"city":{ "id":703448,"name":"Kyiv","coord":{ "lat":50.4333,"lon":30.5167},"country":"UA","population":2514227,"timezone":7200,"sunrise":1670997059,"sunset":1671026049} }
        #endregion
    }
    //public DateTime UnixTimeToDateTime(double UnixTime)
    //{
    //    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
    //    return origin.AddSeconds(UnixTime);
    //}
}
