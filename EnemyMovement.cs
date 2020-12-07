using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] players;
    [SerializeField]
    public float speed;
    private Transform target;

    

    //steps
    //1 look at both players and determine who is closest to you
    //look at target
    //move to target

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        target = FindNearest(players);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) 
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else
           target = FindNearest(players);
       
    }

   

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


    public void setPlayers(GameObject[] go)
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

  

}
