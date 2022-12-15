using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForecastInfo : MonoBehaviour
{
    public InfoForForecast[] list;
}
public class InfoForForecast
{
    public int dt;
    public main main;
    public weatherForForecastInfo[] weather;
}
public class weatherForForecastInfo
{
    public int id;
}
//public class city
//{
//    public int id;
//    public string name;
//    public coord coord;
//    public string country;
//    public int timezone;
//    public int sunrise;
//    public int sunset;
//}
