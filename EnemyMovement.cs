using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Serialized Fields///////////////////////
    [SerializeField]
    private Transform[] players;

    [SerializeField]
    private float speed;

    //Private Fields/////////////////
    private Transform target;

    

   

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        target = FindNearest(players);
    }

    /* Update
     * Check if target and mvoe to target.
     * If lost target, find a new one.
     */
    void Update()
    {
        if (target != null) 
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else
           target = FindNearest(players);
       
    }

   
    /* FindNearest 
     * @param t, players array.
     * 1. check distances and return transform closest.
     * 2.If either palyers are null return player that is not null.
     */
    private Transform FindNearest(Transform[] t)
    { 
            if (players[0] && players[1] != null)
            {
                //check the distances
                if (Vector3.Distance(t[0].position, this.transform.position) >= Vector3.Distance(t[1].position, this.transform.position))
                {
                    return t[0];
                }
                else
                {
                    return t[1];
                }
            }
            else if (players[0] == null)
            {
               return  t[1];
            }
            else if (players[1] == null)
            {
                return t[0];
            }
           
            return new GameObject().transform;
        
    }

    /*  SetPlayers
     *  @parm go is the gameobject containing the players.
     *  This method checks to see if the players exist, then it adds
     *  the players to the internal player array.
     *  */
    public void SetPlayers(GameObject[] go)
    {
        players = new Transform[2];
        if (go == null)
        {
            return;
        }
        if (go[0] != null)
            players[0] = go[0].transform;
        if (go[1] != null)
            players[1] = go[1].transform;
    }

    /*OnTriggerEnter2d
     *This funciton is responsible for destroying the enemy upon 
     * collision with the player.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        //destroy self on collide with player
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }else if(other.tag == "Projectile")
        {
            
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().EnemyKilled();
            Destroy(this.gameObject);
        }
    }

}
