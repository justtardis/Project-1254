using UnityEngine;
using UnityEngine.UI;

//Класс кейса
[System.Serializable]
public class Item
{
    public string name; // имя
    public int id;
    public float price; // цена
    public Sprite picture; //картинка
    public int group;
    public bool received;
}
