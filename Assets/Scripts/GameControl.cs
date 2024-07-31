using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System; 

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField] private Text prizeText;
    [SerializeField] private Rows[] rows;
    [SerializeField] private Transform handle;
    private int prizeVal;
    private bool resultsChecked = false;

    void Update()
    {

        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeText.enabled = false;
            resultsChecked = false;
        }

   
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResult();
            prizeText.enabled = true;
            prizeText.text = "Prize: " + prizeVal;
            resultsChecked = true;
        }
    }

    private void OnMouseDown()
    {
       
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine(PullHandle());
        }
    }

    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        resultsChecked = false; 
    }

    public void CheckResult()
    {
        string slot0 = rows[0].stoppedSlot;
        string slot1 = rows[1].stoppedSlot;
        string slot2 = rows[2].stoppedSlot;

        prizeVal = 0; 

 
        if (slot0 == slot1 && slot1 == slot2)
        {
            switch (slot0)
            {
                case "Diamond":
                    prizeVal = 200;
                    break;
                case "Crown":
                    prizeVal = 400;
                    break;
                case "Melon":
                    prizeVal = 600;
                    break;
                case "Bar":
                    prizeVal = 800;
                    break;
                case "Seven":
                    prizeVal = 1500;
                    break;
                case "Cherry":
                    prizeVal = 3000;
                    break;
                case "Lemon":
                    prizeVal = 5000;
                    break;
            }
        }
  
        else
        {
            if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Diamond" ||
                (slot1 == slot2 && slot1 == "Diamond"))
            {
                prizeVal = 100;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Crown" ||
                     (slot1 == slot2 && slot1 == "Crown"))
            {
                prizeVal = 300;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Melon" ||
                     (slot1 == slot2 && slot1 == "Melon"))
            {
                prizeVal = 500;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Bar" ||
                     (slot1 == slot2 && slot1 == "Bar"))
            {
                prizeVal = 700;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Seven" ||
                     (slot1 == slot2 && slot1 == "Seven"))
            {
                prizeVal = 1000;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Cherry" ||
                     (slot1 == slot2 && slot1 == "Cherry"))
            {
                prizeVal = 2000;
            }
            else if ((slot0 == slot1 || slot0 == slot2) && slot0 == "Lemon" ||
                     (slot1 == slot2 && slot1 == "Lemon"))
            {
                prizeVal = 4000;
            }
        }
    }
}