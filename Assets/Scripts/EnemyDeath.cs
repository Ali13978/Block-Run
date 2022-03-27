using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Animator MyAnimator;

    private void Start()
    {
        GetComponent<Animator>();
    }

    public void EnemyDied()
    {
        Destroy(GetComponent<CapsuleCollider2D>());
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 0.05f);
    }
    
}
