using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class WeatherInfo 
{
    public coord coord;
    public weather[] weather;
    [JsonProperty("base")]
    public string Base;
    public main main;
    public float visibility;
    public wind wind;
    public clouds clouds;
    public float dt;
    public sys sys;
    public int id;
    public string name;
    public float cod;
    //[JsonProperty("weather")]
    

    //{"coord":{"lon":30.5167,"lat":50.4333},"weather":[{"id":500,"main":"Rain","description":"light rain","icon":"10d"}],"base":"stations","main":{ "temp":280.22,"feels_like":280.22,"temp_min":279.05,"temp_max":280.22,"pressure":997,"humidity":94},"visibility":1293,"wind":{ "speed":0.89,"deg":137,"gust":1.79},"rain":{ "1h":0.25},"clouds":{ "all":100},"dt":1670751440,"sys":{ "type":2,"id":2003742,"country":"UA","sunrise":1670737692,"sunset":1670766852},"timezone":7200,"id":703448,"name":"Kyiv","cod":200}
}
public class coord
{
    public float lon;
    public float lat;
}
public class weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}
public class main
{
    float _pressure;
    public float pressure { get => _pressure; set => _pressure = value / 1.3332239f; }
    float _temp;

    public float temp { get => _temp; set => _temp = value - 273.15f; }
    public float humidity;
    float _tempMin;
    public float tempMin { get => _tempMin; set => _tempMin = value - 273.15f; }

    float _tempMax;
    public float tempMax { get => _tempMax; set => _tempMax = value - 273.15f; }

}
public class wind
{
    public float speed;
    public float deg;
    public float gust;
}
public class clouds
{
    public int all;
}
public class sys
{
    public float type;
    public int id;
    public double message;
    public string country;
    public float sunrise;
    public float sunset;
}
