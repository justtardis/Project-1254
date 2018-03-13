using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoney : MonoBehaviour {

    public RectTransform rect;
    public Game g;
    public int id;
    public int countNumber;
    public Vector2 curPos;
	// Use this for initialization
	void Start () {
        curPos = rect.localPosition;
        countNumber = id == 0 ? getCountsOfDigits(g.silver) : getCountsOfDigits(g.gold);
        switch (countNumber)
        {
            case 1:
                {
                    rect.localPosition = new Vector2(40f, curPos.y);
                    break;
                }
            case 2:
                {
                    rect.localPosition = new Vector2(5f, curPos.y);
                    break;
                }
            case 3:
                {
                    rect.localPosition = new Vector2(-30f, curPos.y);
                    break;
                }
            case 4:
                {
                    rect.localPosition = new Vector2(-65f, curPos.y);
                    break;
                }
            case 5:
                {
                    rect.localPosition = new Vector2(-100f, curPos.y);
                    break;
                }
        }
    }

    public int getCountsOfDigits(int number)
    {
        string count = number.ToString();
        return countNumber = count.Length;
    }
    
}
