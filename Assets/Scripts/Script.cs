using System;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public Text timerText;
    private float timeStart = 80;
    private bool gameOver = false;

    public Text pin1;
    public Text pin2;
    public Text pin3;
    public Text dialogText;
    
    public GameObject objectPin1;
    public GameObject objectPin2;
    public GameObject objectPin3;
    public GameObject endGamePanel;
    public GameObject winGamePanel;
    public GameObject dialogPanel;
    
    private int p1;
    private int p2;
    private int p3;

    // Start is called before the first frame update
    void Start()
    {
        dialogText = dialogPanel.GetComponentInChildren<Text>();
        timerText.text = timeStart.ToString();
        p1 = Convert.ToInt32(pin1.text);
        p2 = Convert.ToInt32(pin2.text);
        p3 = Convert.ToInt32(pin3.text);
    }

    public void StartGame()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
        pin1.text = p1.ToString();
        pin2.text = p2.ToString();
        pin3.text = p3.ToString();
        if (Mathf.Round(timeStart) == 0)
        {
            gameOver = true;
            EndGameHandler();
        }
        if (Mathf.Round(timeStart) == 60)
        {
            if (!dialogPanel.activeSelf) { DialogMenuHandler(1); }
        }
        if (Mathf.Round(timeStart) == 15)
        {
            if (!dialogPanel.activeSelf) DialogMenuHandler(2);
        }
        if (Mathf.Round(timeStart) == 55 || timeStart == 10)
        {
            if (dialogPanel.activeSelf) DialogMenuHandler(false);
        }
        if((p1 == p2) && ( p1 == p3))
        {
            WinGameHandler();
        }
    }

    public void Hammer()
    {
        p1 = UpdatePin(p1, 1,'+', objectPin1);   
        p2 = UpdatePin(p2, 2, '+', objectPin2);
        p3 = UpdatePin(p3, 1, '+', objectPin3); ;
    }

    public void Lockpick()
    {
        p1 = UpdatePin(p1, 1, '-', objectPin1);
        p2 = UpdatePin(p2, 0, '+', objectPin2);
        p3 = UpdatePin(p3, 2, '+', objectPin3); ;
    }

    public void Screwdriver()
    {
        p1 = UpdatePin(p1, 1, '+', objectPin1);
        p2 = UpdatePin(p2, 1, '-', objectPin2);
        p3 = UpdatePin(p3, 1, '-', objectPin3);
    }

    private int UpdatePin(int pin, int value, char action, GameObject objectPin)
    {
        int tmp = pin;
        if (action == '+')
        {
            pin += value;
            if (pin > 10) { pin = 10; }
        } 
        else 
        {
            pin -= value;
            if (pin < 0) { pin = 0; }
        }
        
        int MoveResult = tmp - pin;

        if (MoveResult < 0)
        {
            for (int i = 0; i < Mathf.Abs(MoveResult); i ++ )
            {
                objectPin.transform.SetLocalPositionAndRotation(new Vector3(objectPin.transform.localPosition.x, objectPin.transform.localPosition.y + 5), new Quaternion());
            }
        }
        else if (MoveResult > 0)
        {
            for (int i = 0; i < Mathf.Abs(MoveResult); i++)
            {
                objectPin.transform.SetLocalPositionAndRotation(new Vector3(objectPin.transform.localPosition.x, objectPin.transform.localPosition.y - 5), new Quaternion());
            }
        }

        return pin;
    }

    private void DialogMenuHandler(int value)
    {
        dialogPanel.SetActive(true);
        if (value == 1)
        {
            dialogText.text = "Неволнуйся. У тебя получится. Одна бабушка уже положила глаз, так что у тебя минута!"; 
        }
        if (value == 2)
        {
            dialogText.text = "Осталось 15 секунд... Если не успешь, то не хватит времени выкрутить запчастию. Но у тебя получится!";

        }
    }

    private void DialogMenuHandler(bool activate)
    {
        dialogPanel.SetActive(activate);
    }

    private void EndGameHandler()
    {
        gameOver = false;
        endGamePanel.SetActive(true);
        this.enabled = false;
    }

    private void WinGameHandler()
    {
        winGamePanel.SetActive(true);
        this.enabled = false;
    }

    public void ResetGame()
    {
        if (this.enabled == false)
        {
            this.enabled = true;
        }

        timeStart = 80;
        p1 = 5;
        p2 = 4;
        p3 = 1;
        ResetPinPos();
    }

    private void ResetPinPos()
    {
        objectPin1.transform.SetLocalPositionAndRotation(new Vector3(objectPin1.transform.localPosition.x, 0), new Quaternion());
        objectPin2.transform.SetLocalPositionAndRotation(new Vector3(objectPin2.transform.localPosition.x, -5), new Quaternion());
        objectPin3.transform.SetLocalPositionAndRotation(new Vector3(objectPin3.transform.localPosition.x, -20), new Quaternion());
    }
}
