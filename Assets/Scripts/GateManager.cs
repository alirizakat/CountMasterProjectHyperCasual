using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateManager : MonoBehaviour
{
    public GameObject gate;
    public Text gateText;
    public int randomNumber;
    public bool multiply;
    public bool negative;
    // Start is called before the first frame update
    void Start()
    {
        PickRandomForGate();
    }
    void PickRandomForGate()
    {
        if(multiply)
        {
            randomNumber = Random.Range(1,3);
            gateText.text = "X" + randomNumber.ToString();
        }
        else
        {
            randomNumber = Random.Range(1, 11);
            if(randomNumber % 2 != 0)
            {
                randomNumber+=1;
                gateText.text = randomNumber.ToString();
            }
            else
            {
                gateText.text = randomNumber.ToString();
            }
        }
    }
}
