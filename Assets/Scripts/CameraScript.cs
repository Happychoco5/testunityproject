using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float camSpeed = 100f;

    public int MIN_x, MAX_x, MIN_y, MAX_y;
    public int MIN_Size;
    public int MAX_Size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
            }
        }

        //If getting the horizontal axis, move left and right based on the current camera speed.
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position += (Vector3.right * Input.GetAxis("Horizontal") * camSpeed) * Time.deltaTime;
        }
        //If getting the vertical, move up and down based on the current camera speed.
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position += (Vector3.forward * Input.GetAxis("Vertical") * camSpeed) * Time.deltaTime;
        }
        /*
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize + 1, MIN_Size, MAX_Size);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize - 1, MIN_Size, MAX_Size);
        }
        */
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MIN_x, MAX_x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, MIN_y, MAX_y)
            );
    }
}
