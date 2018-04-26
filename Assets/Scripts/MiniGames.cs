using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGames : MonoBehaviour {

    // Первая игра будет крэш
    // Нарастание от 0 до 200x
    // Обрываем график  в рандомном значении
    public float _counter = 0f;
    public float _timer = 0.05f;
    public Text crashCounter;
    public bool b = false;
    public void ss()
    {
        if (!b) b = true;
        else b = false;
    }
    private void FixedUpdate()
    {
        if (b)
        {
            crashCounter.text = (Mathf.Lerp(1f, 200f, _timer * Time.deltaTime)).ToString("0.#0");
            _timer += _timer* 0.01f;
        }
        
    }
}
