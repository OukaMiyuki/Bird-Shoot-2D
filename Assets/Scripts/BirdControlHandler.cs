using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdControlHandler : MonoBehaviour {

    private bool enableDrag = true; //enable disable drag

    public void birdEnableController(bool enable) {
        enableDrag = enable;
    }

    private void OnMouseDown() { //event when mouse is clicked
        if (enableDrag) { //if drag is snebled
            SendMessage("spriteHover");
        }
        else {
            return;
        }
    }

    private void OnMouseDrag() { //event when mouse is dragged after it's clicked
        if (enableDrag) { //if drag enabled
            SendMessage("dragController");
        }
        else {
            return;
        }
    }

    private void OnMouseUp() { //event when mouse is released
        if (enableDrag) { //if drag is enabled
            SendMessage("launchTheBird");
        }
        enableDrag = false; //disable drag after mouse releashed
    }
}