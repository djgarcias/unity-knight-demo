using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //VARIABLES

    [SerializeField] private float mouseSensibility;

    //REFERENCES
    private Transform parent;
    

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;

        parent.localEulerAngles = new Vector3(parent.localEulerAngles.x, parent.localEulerAngles.y + mouseX, parent.localEulerAngles.z);

        //parent.Rotate(Vector3.up, mouseX);
    }
}
