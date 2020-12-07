using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int playerInt;
    private Rigidbody2D rb2d;
    private Vector2 moveForce;
    [SerializeField]
    private float force = 1;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileVelocity = 1;
    private BoxCollider2D col;
    public int lives;
    private Vector3 startPosition;
    [SerializeField]
    private Text lifeText;
  

    // Start is called before the first frame update
    void Start()
    {
       rb2d =  GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        startPosition = this.transform.position;
        lives = 3;
        lifeText.text = "Lives: X X X";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInt == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveForce = new Vector2(-1, 0);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                moveForce = new Vector2(1, 0);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                moveForce = new Vector2(0, 1);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                moveForce = new Vector2(0, -1);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }
        }else if(playerInt == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveForce = new Vector2(-1, 0);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveForce = new Vector2(1, 0);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveForce = new Vector2(0, 1);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveForce = new Vector2(0, -1);
                rb2d.AddForce(moveForce * force);
                FireProjectile(moveForce);
            }
        }

    }


    private void FireProjectile(Vector2 direction)
    {
        Quaternion rot = transform.rotation;
        if(direction.x != 0)
        {
            rot = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        

        GameObject obj = Instantiate(projectile, transform.position, rot);
        Rigidbody2D obj2d = obj.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(obj.GetComponent<BoxCollider2D>(), col);
        Projectile pr = obj.GetComponent<Projectile>();
        pr.SetPlayerInt(playerInt);
     
        obj2d.velocity = -direction * projectileVelocity;
    }

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

        this.transform.position = startPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }


}
