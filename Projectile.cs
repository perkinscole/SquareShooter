using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   

    private int playerInt = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
   
        if ((other.gameObject.tag == "Player1" && playerInt == 2)){
            Destroy(this.gameObject);
        }else if((other.gameObject.tag == "Player2" && playerInt == 1))
        {
            Destroy(this.gameObject);
        }else if(other.gameObject.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().EnemyKilled();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void SetPlayerInt(int i)
    {
        this.playerInt = i;
    }
}
