using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col) {
        Bird bird = col.collider.GetComponent<Bird>();
        if(bird != null || col.contacts[0].normal.y < -0.5 || col.relativeVelocity.magnitude > 15) { //if enemy collide with the bird (player) or collide with object from the top of it (-0.5) or the power of velocity greater than 15 (more value = more power)
            Destroy(gameObject); //destroy the enemy object
        }

        Enemy enemy = col.collider.GetComponent<Enemy>();
        if (enemy != null) { //if enemy collide with enemy
            return;
        }
    }
}
