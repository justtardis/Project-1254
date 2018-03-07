using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public float silver; // серебро
    public float gold; // золото
    public Case[] cases; //кейсы
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

//Класс кейса
[System.Serializable]
public class Case
{
    public int id; 
    public string name; // имя
    public float price; // цена
    public Item[] items;
    public Sprite picture; //картинка
}

//Класс кейса
[System.Serializable]
public class Item
{
    public int id;
    public string name; // имя
    public float price; // цена
    public Sprite picture; //картинка
}