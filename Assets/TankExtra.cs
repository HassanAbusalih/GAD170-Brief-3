using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TankExtra : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;
    public Tank tank1;
    public Tank tank2;
    public TankExtra otherTank;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winText.gameObject.SetActive(false);
        score = 0;
        ChangeScore();
    }

    // Update is called once per frame
    void Update()
    {
        pos = GetComponent<Transform>().position;
        if (Input.GetKeyDown(KeyCode.Space) && pos.y < 1 && tank1.playerNumber == PlayerNumber.One)
        {
            rb.velocity += new Vector3 (0, 10, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightShift) && pos.y < 1 && tank1.playerNumber == PlayerNumber.Two)
        {
            rb.velocity += new Vector3(0, 10, 0);
        }
        
    }

    void ChangeScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            score++;
            ChangeScore();
        }
    }

    public void DecideWinner()
    {
        if (score > otherTank.score)
        {
            winText.text = "Player " + tank1.playerNumber.ToString() + " wins!";
            winText.gameObject.SetActive(true);
            Invoke("ResetGame", 5f);
        }
        else
        {
            winText.text = "Player " + tank2.playerNumber.ToString() + " wins!";
            winText.gameObject.SetActive(true);
            Invoke("ResetGame", 5f);
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
