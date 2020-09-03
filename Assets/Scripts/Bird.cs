using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    private Vector3 _initialPosition; //define vector 3 variable to store the current position of an object based on x y z axes
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField] private float _launchPower = 500;

    private void Awake() {
        _initialPosition = transform.position; //get the current transform of the bird object (first transform when it hasn't been launched yet)
    }

    private void Update() {
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) { //if the bird has already been launched or in other words, it's on the ground
            _timeSittingAround += Time.deltaTime; //count the time to load the next scene, it's the same as when you load scene then put a time on it (using invoke Invoke("loadScene", 2f);)
        }

        if (transform.position.y > 10 || transform.position.y < -10 || transform.position.x > 10 || transform.position.x < -10 || _timeSittingAround > 3) { //load scene when bird go beyond the y axes limit and x axes lmit and also when the bird hit the gound or already launched
            string currentSceneName = SceneManager.GetActiveScene().name; //get the active scene
            SceneManager.LoadScene(currentSceneName); //load the scene based on the active one
        }
    }

    private void OnMouseDown() { //event when mouse is clicked
        GetComponent<SpriteRenderer>().color = Color.red; //change the color of the sprite to red
    }

    private void OnMouseDrag() { //event when mouse is dragged after it's clicked
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // to get the screeen to world position of mouse position  (screen to world is basically the distance betweeen the camera and the world, without this your bird will be put behind the backgound when you drag it)
        transform.position = new Vector3(newPosition.x, newPosition.y, 0); //set the nye transform position when it dragged to (x, y, and z) why z is 0? because in 2d game you only need to move up, down and left to right you don't need to concern about the z position unless it's a 3D game
    }

    private void OnMouseUp() { //event when mouse is released
        GetComponent<SpriteRenderer>().color = Color.white; // change back the color of the sprite to red 9original color)
        Vector2 directionToInitialPosition = _initialPosition - transform.position; //get the distance betewwen fiorst position and position when the bird is dragged
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower); // multiply it with _launchPower to determine how much power for the force
        GetComponent<Rigidbody2D>().gravityScale = 1; //set the gravity scale back to 1 so the bird will fall down after it's launched
        _birdWasLaunched = true; // set the bird launched to true, so it'll know that the bird has been launched or in the ground (line 17)
    }
}