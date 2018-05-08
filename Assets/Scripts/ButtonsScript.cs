using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour {

    public GameObject Lottery;
    public GameObject MiniGames;
    public Transform Line;
    public Vector2[] pos; // 0 - лотерея, 1 - игры
    public bool[] posBool = {false, false};
    public Vector2[] width;// 0 - лотерея, 1 - игры
    RectTransform rt;

    // Получаем компонент трансформа для того, чтобы увправлять параметрами объекта (ширина, высота)
    void Start () {
        rt = Line.GetComponent<RectTransform>();        
	}
	
    // Думаю, тут все понятно. 0 - лотерея, 1 - игры
    public void Click(int panel)
    {
        if(panel == 0)
        {
            if (!Lottery.activeSelf)
            {
                Lottery.SetActive(true);
                MiniGames.SetActive(false);
                posBool[0] = true;
                posBool[1] = false;
            }            
        }
        else if (panel == 1)
        {
            if (!MiniGames.activeSelf)
            {
                Lottery.SetActive(false);
                MiniGames.SetActive(true);
                posBool[1] = true;
                posBool[0] = false;
            }
        }

    }
	// Update is called once per frame
	void Update () {
        // если нажали на "Игры, то
        if (posBool[1])
        {
            // записываем в текущие параметры размерности вектор ширины и высоты с некоторой скоростью. 
            // sizeDelta отвечает за ширину и высоту объекта.
            // Lerp нужен для плавного изменения размерности от настоящей до целевой.
            // Нельзя указывать "от размера А до размера Б - width[0] -> width[1], потому что тогда каждый кадр объект будет принимать за начальное
            // состояние исходное положение и апдейт просто чокнется.
            // с перемещением аналогично
            rt.sizeDelta = Vector2.Lerp(rt.sizeDelta, width[1], 5 * Time.deltaTime);
            // Перемещение объекта тоже делаем через Лерп. Обязательно в локальных координатах, ибо мировые через чур большие.
            // Перемещение так же от текущей позиции до указанной за время t.
            Line.localPosition = Vector2.Lerp(Line.localPosition, pos[1], 5 * Time.deltaTime);
            // достигнув близкой координаты к цели мы останавливаем перемещение изменением флага и заданием точной ширины и положения
            // Пользователь не заметит резкого скачка к конечной точке, а нам проще будет вести систему отсчета
            if (Line.localPosition.x >= 246.5f)
            {
                posBool[1] = false;
                rt.sizeDelta = width[1];
                Line.localPosition = pos[1];
            }
        }
        if (posBool[0])
        {
            Line.localPosition = Vector2.Lerp(Line.localPosition, pos[0], 5 * Time.deltaTime);
            rt.sizeDelta = Vector2.Lerp(rt.sizeDelta, width[0], 5 * Time.deltaTime);
            if (Line.localPosition.x <= -236.4f)
            {
                posBool[0] = false;
                rt.sizeDelta = width[0];
                Line.localPosition = pos[0];
            }
        }
    }
}
