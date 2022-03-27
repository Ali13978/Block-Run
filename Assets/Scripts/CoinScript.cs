using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    Animator MyAnimator;
    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            GetComponent<AudioSource>().Play();
            CoinGained();
            Destroy(GetComponent<CircleCollider2D>());
        }
    }

    private void CoinGained()
    {
        FindObjectOfType<DeathCollider>().IncrementCoins();
        MyAnimator.SetBool("IsGained", true);
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

}
