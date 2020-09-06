using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birdLifeState : MonoBehaviour {

    [SerializeField] private int lifes = 3; //number of playe'rs life

    private Vector3 _initialPosition = new Vector3(-10.61f, -6.9f, 0f);

    BirdControlHandler birdControl;

    void Start() {
        birdControl = FindObjectOfType<BirdControlHandler>();
    }

    public void birdState() {
        lifes -= 1;
        if (lifes < 1) { //if life's runs out
            print("finish");
            birdFinishState(true);
        } else {
            print("not yet");
            birdFinishState(false);
        }
    }

    private void birdFinishState(bool finish) {
        if (finish) { //if life's runs out
            birdControl.birdEnableController(true);
            print("dead");
            string currentSceneName = SceneManager.GetActiveScene().name; //get the active scene
            SceneManager.LoadScene(currentSceneName); //load the scene based on the active one
        } else {
            transform.position = _initialPosition; //initialize the current transform to initial transform
            print(_initialPosition);
            transform.localRotation = Quaternion.identity; //reset rotation to 0
            GetComponent<Rigidbody2D>().gravityScale = 0; //change the gravity scale back to 0, so the player will be floating in the air
            _initialPosition = transform.position; //re-define the initial transform to current transform
            birdControl.birdEnableController(true);
        }
    }
}
