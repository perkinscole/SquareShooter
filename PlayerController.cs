using UnityEngine;
using UnityEngine.UI;
/**
 *@author Cole Perkins
 * The Player Controller Script is responsible for moving responding to user
 * input related to character keys and moving the character in the appropriate way.
 * It is also responsible for Character Collision Detection and Firing the
 * Projectiles.
 * */
public class PlayerController : MonoBehaviour
{
    //SERIALIZED FIELDS////////////////////////////
    [SerializeField]
    private int playerInt; //Player Identification integer-> used for projectile

    [SerializeField]
    private float force; //force to apply to player on move

    [SerializeField]
    private GameObject projectile; //What the player shoots

    [SerializeField]
    private float projectileVelocity;

    [SerializeField]
    private int lives; //player number of lives

    [SerializeField]
    private Text lifeText; //the UI text for displaying player lives remaining.

    //PRIVATE FIELDS////////////////////////////////
    private Rigidbody2D rb2d;  //Rigidbody for moving the player

    private Vector2 moveDirection; //stores move direction on input

    private BoxCollider2D col; //player collider
  
    private Vector3 startPosition; //player start position




    // Initialzize values on start
    void Start()
    {
        rb2d =  GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        startPosition = this.transform.position;
        lifeText.text = "Lives: X X X";
        
    }

    /* *
     * Updatde()
     *  Steps in update
     *  1. Check which player we are controlling and respond to appropriate keys
     *  2. Get Keycode
     *  3. Set the moveDirection, apply the force to the player, Fire the projectile.
     *  */
    void Update()
    {

        if (playerInt == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveDirection = new Vector2(-1, 0);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                moveDirection = new Vector2(1, 0);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                moveDirection = new Vector2(0, 1);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                moveDirection = new Vector2(0, -1);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }
        }else if(playerInt == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDirection = new Vector2(-1, 0);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDirection = new Vector2(1, 0);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection = new Vector2(0, 1);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDirection = new Vector2(0, -1);
                rb2d.AddForce(moveDirection * force);
                FireProjectile(moveDirection);
            }
        }

    }

    /* FireProjectile
     * @param direction is the direction to fire the projectile.
     *  steps in FireProjectile,
     *  1. Determine if the bullet has been fired horizonitally,
     *     1a. If it has, set a rotation to 90 degrees.
     *  2. Instantiate a projectile
     *  3. Set its velocity.
     */
    private void FireProjectile(Vector2 direction)
    {
        Quaternion rot = transform.rotation; //rotation placeholder
        if(direction.x != 0)// check if horizontal
        {
            rot = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        
        //instantiate projectiles
        GameObject obj = Instantiate(projectile, transform.position, rot);
        Rigidbody2D obj2d = obj.GetComponent<Rigidbody2D>();

        //ignore collision with player
        Physics2D.IgnoreCollision(obj.GetComponent<BoxCollider2D>(), col);
       
       
        //set velocity opposite player direction
        obj2d.velocity = -direction * projectileVelocity;
    }


    /*OnTriggerEnter2D 
     * This function determines if a character is still alive or not.
     * 1. If this function is triggered then character loses a life and updates
     *    ui text accordingly.
     * 2. If the character is not dead it resets tehir position and velocity.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        lives = lives-1;

        if (lives == 2)
        {
            lifeText.text = "Lives: X X -";
        }
        else if (lives == 1)
        {
            lifeText.text = "Lives: X - -";
        }
        else if (lives <= 0)
        {
            lifeText.text = "Lives: - - -";
            Destroy(this.gameObject);
        }

        //if not dead reset position and velocity
        this.transform.position = startPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        
    }


}
