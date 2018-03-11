using UnityEngine;
using UnityEngine.UI;

//Класс кейса
[System.Serializable]
public class Item
{
    public int id;
    public string name; // имя
    public float price; // цена
    public Sprite picture; //картинка
    public int group;
}
