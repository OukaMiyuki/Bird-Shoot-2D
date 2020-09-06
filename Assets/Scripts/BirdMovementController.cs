using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovementController : MonoBehaviour {

    private Vector3 _initialPosition; //define vector 3 variable to store the current position of an object based on x y z axes
    private bool _birdWasLaunched = false; //bird launched state

    [SerializeField] private float _launchPower = 500; //velocity power
    [SerializeField] private float xRange = 13f; //slingshoot xrange

    private float _timeSittingAround;

    private void Awake() {
        _initialPosition = transform.position; //get the current transform of the bird object (first transform when it hasn't been launched yet)
    }

    private void Update() {
        GetComponent<LineRenderer>().SetPosition(0, transform.position); // line renderer from initial position to current transform
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition); // line renderer inituial state
    }

    private void spriteHover() {
        GetComponent<SpriteRenderer>().color = Color.red; //change the color of the sprite to red
        GetComponent<LineRenderer>().enabled = true; //set the line rendere to true when it clicked
    }

    private void dragController() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // to get the screeen to world position of mouse position  (screen to world is basically the distance betweeen the camera and the world, without this your bird will be put behind the backgound when you drag it)
        float clampedXPos = Mathf.Clamp(newPosition.x, -xRange, -6f); //clamp position for slingshoot xrange 13f to left -6f to right
        float clampedYPos = Mathf.Clamp(newPosition.y, -10f, -4.5f); //clamp position for slingshoot yrange -10f top-down -3f down to top
        transform.position = new Vector3(clampedXPos, clampedYPos, 0); //set the new transform position when it dragged to (x, y, and z) why z is 0? because in 2d game you only need to move up, down and left to right you don't need to concern about the z position unless it's a 3D game
    }

    private void launchTheBird() {
        GetComponent<SpriteRenderer>().color = Color.white; // change back the color of the sprite to red 9original color)
        Vector2 directionToInitialPosition = _initialPosition - transform.position; //get the distance betewwen fiorst position and position when the bird is dragged
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower); // multiply it with _launchPower to determine how much power for the force
        GetComponent<Rigidbody2D>().gravityScale = 1; //set the gravity scale back to 1 so the bird will fall down after it's launched
        _birdWasLaunched = true; // set the bird launched to true, so it'll know that the bird has been launched or in the ground (line 17)
        GetComponent<LineRenderer>().enabled = false; //set the line renderer to false or disable it after the mouse is releashed
        birdLaunched();
    }

    private void birdLaunched() {
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) { //if the bird has already been launched or in other words, it's on the ground
            Invoke("callBirdState", 7f);
        }
    }

    private void callBirdState() {
        SendMessage("birdState");
    }
}
