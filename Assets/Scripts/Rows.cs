using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Rows : MonoBehaviour
{
    private int randomVal;
    private float timeInt;

    public bool rowStopped;
    public string stoppedSlot;

    private readonly float[] slotPositions = new float[] { -35f, -22f, -9f, 4f, 17f, 30f, 43f, 56f };

    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInt = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (transform.position.y <= -35f)
            {
                transform.position = new Vector2(transform.position.x, 56f);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - 4f);
            yield return new WaitForSeconds(timeInt);
        }

        randomVal = Random.Range(60, 100);
        switch (randomVal % 3)
        {
            case 1:
                randomVal += 2;
                break;
            case 2:
                randomVal += 1;
                break;
        }

        for (int i = 0; i < randomVal; i++)
        {
            if (transform.position.y <= -35f)
            {
                transform.position = new Vector2(transform.position.x, 56f);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - 4f);

            if (i > Mathf.RoundToInt(randomVal * 0.25f))
                timeInt = 0.05f;
            if (i > Mathf.RoundToInt(randomVal * 0.5f))
                timeInt = 0.1f;
            if (i > Mathf.RoundToInt(randomVal * 0.75f))
                timeInt = 0.15f;
            if (i > Mathf.RoundToInt(randomVal * 0.95f))
                timeInt = 0.2f;

            yield return new WaitForSeconds(timeInt);
        }

        SnapToClosestSlot();
        DetermineStoppedSlot();
        rowStopped = true;
    }

    private void SnapToClosestSlot()
    {
        float closestY = slotPositions[0];
        float minDistance = Mathf.Abs(transform.position.y - slotPositions[0]);

        foreach (float posY in slotPositions)
        {
            float distance = Mathf.Abs(transform.position.y - posY);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestY = posY;
            }
        }

        transform.position = new Vector2(transform.position.x, closestY);
    }

    private void DetermineStoppedSlot()
    {
        float posY = transform.position.y;

        if (posY == -35f)
            stoppedSlot = "Diamond";
        else if (posY == -22f)
            stoppedSlot = "Crown";
        else if (posY == -9f)
            stoppedSlot = "Melon";
        else if (posY == 4f)
            stoppedSlot = "Bar";
        else if (posY == 17f)
            stoppedSlot = "Seven";
        else if (posY == 30f)
            stoppedSlot = "Cherry";
        else if (posY == 43f)
            stoppedSlot = "Lemon";
        else if (posY == 56f)
            stoppedSlot = "Diamond";
    }

    void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}