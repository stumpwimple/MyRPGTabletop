using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject selectionRing;

    public GameObject FPSCam;
    public GameObject OrbitCam;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V)) {
            if (OrbitCam.active)
            {
                OrbitCam.active = false;
                FPSCam.active = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
               OrbitCam.active = true;
               FPSCam.active = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }



            if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit) && rayHit.collider.gameObject.GetComponent<TurnOffer>())
            {
                rayHit.collider.gameObject.GetComponent<TurnOffer>().go.active = !rayHit.collider.gameObject.GetComponent<TurnOffer>().go.active;
            }
            else if (Physics.Raycast(ray, out rayHit) && rayHit.collider.gameObject != selectedObject && rayHit.collider.gameObject.name != "Terrain")
            {
                selectedObject = rayHit.collider.gameObject;
                selectionRing.transform.position = new Vector3(selectedObject.transform.position.x, 0, selectedObject.transform.position.z);
                Debug.Log("You've Selected: " + selectedObject);

            }
            else if (Physics.Raycast(ray, out rayHit) && rayHit.collider.gameObject == selectedObject)
            {
                Camera.main.transform.LookAt(selectedObject.transform);
                selectedObject = null;
                selectionRing.transform.position = new Vector3(2, -1, 2);
                Debug.Log("You've centered View on: " + selectedObject);
            }
            else if (Physics.Raycast(ray, out rayHit) && selectedObject && selectedObject.GetComponent<Player>() && rayHit.collider.gameObject.name == "Terrain") {
                Debug.Log("You've Tapped the terrain at: " + rayHit.point);
                GameObject moveTarget = new GameObject();
                moveTarget.transform.position = rayHit.point;
                selectedObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = moveTarget.transform;
                selectedObject = null;
                selectionRing.transform.position = new Vector3(2, -1, 2);
            }
            else
            {
                selectedObject = null;
                selectionRing.transform.position = new Vector3(2, -1, 2);
                Debug.Log("You've Deselected Object");
            }
        }
    }
}
