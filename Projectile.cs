using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Projectile
 * @author Cole Perkins
 * The projectile fired by the player.
 * It is responsible for destroying itself.
 */
public class Projectile : MonoBehaviour
{

    /* OnTriggerEnter2D
     * This simply destroys the projectile when
     * colliding with anything.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
            Destroy(this.gameObject);
        
    }

    
}
