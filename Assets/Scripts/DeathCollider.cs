using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    int Coins = 0;
    int Lives = 3;
    int SceneIndex = 0;
    public TextMeshProUGUI Coins1;
    public TextMeshProUGUI Lifes;
    GameObject Player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            DecrementLives();
            Player = collision.gameObject;
            Player.GetComponent<player>().ContinueMovement();
            Player.GetComponent<player>().Respawn();
        }
    }

    public void DecrementLives()
    {
        Lives--;
    }

    private void RestartScene()
    {
        if (Lives <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }

    private void Update()
    {
        Lifes.text = "x " + Lives.ToString();
        Coins1.text = Coins.ToString();
        RestartScene();
    }

    public void IncrementCoins()
    {
        Coins++;
    }
}
