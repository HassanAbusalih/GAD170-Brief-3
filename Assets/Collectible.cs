using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    private Vector3 pos;
    public TextMeshProUGUI Objective;
    private int count;
    private int objectiveCount;
    private Rigidbody rb;
    private Color color;
    public TankExtra CheckWinner;
    // Start is called before the first frame update
    void Start()
    {
        NewCollectible();
        Objective.gameObject.SetActive(false);
        count = 0;
        objectiveCount = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Transform>().position.y < 2.5f && rb.velocity.y < 3.5f)
        {
            rb.velocity += new Vector3 (0, 0.05f, 0);
        }
        else if (GetComponent<Transform>().position.y > 2.5f && rb.velocity.y > -3.5f)
        {
            rb.velocity -= new Vector3(0, 0.05f, 0);
        }

        if (count == 3 && objectiveCount == 0)
        {
            objectiveCount++;
            Objective.gameObject.SetActive(true);
            Objective.text = "Move to Zone 2 (South)";
            Invoke("TurnOffObjective", 5f);
        }
        if (count == 6 && objectiveCount == 1)
        {
            objectiveCount++;
            Objective.gameObject.SetActive(true);
            Objective.text = "Move to Zone 3 (West)";
            Invoke("TurnOffObjective", 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NewCollectible();
        }
    }

    public void NewCollectible()
    {
        count++;
        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        GetComponent<MeshRenderer>().material.color = color;
        if (count < 3)
        {
            pos = new Vector3(Random.Range(-30f, 30f), 1.5f, Random.Range(-30f, 30f));
            GetComponent<Transform>().position = pos;
        }
        else if (count < 6)
        {
            pos = new Vector3(Random.Range(-140f, -80f), 1.5f, Random.Range(-30f, 30f));
            GetComponent<Transform>().position = pos;
        }
        else if (count < 9)
        {
            pos = new Vector3(Random.Range(-140f, -80f), 1.5f, Random.Range(80f, 140f));
            GetComponent<Transform>().position = pos;
        }
        else if (count >= 9)
        {
            gameObject.SetActive(false);
            CheckWinner.DecideWinner();
        }
    }

    void TurnOffObjective()
    {
        Objective.gameObject.SetActive(false);
    }
}
