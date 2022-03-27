using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 1f;
    Rigidbody2D MyRigidBody;
    CapsuleCollider2D MyCapsuleCollider;
    bool canEnemyMove = true;
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        MyCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }
                                            
    void Update()                           
    {                                       
            Movement();                     
    }                                       
                                            
    public void CannotMove()                
    {                                       
        canEnemyMove = false;               
    }                                       
                                            
    private void Movement()                 
    {   if (canEnemyMove == true)
        {
            if (IsFacingRight())
            {
                MyRigidBody.velocity = new Vector2(MovementSpeed, 0f);
            }
        
            else
            {
                MyRigidBody.velocity = new Vector2(-MovementSpeed, 0f);
            }
        }
        else
        {
            if (IsFacingRight())
            {
                MyRigidBody.velocity = new Vector2(0f, 0f);
            }

            else
            {
                MyRigidBody.velocity = new Vector2(-0f, 0f);
            }
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D Collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(MyRigidBody.velocity.x)), 1f);
    }


}
