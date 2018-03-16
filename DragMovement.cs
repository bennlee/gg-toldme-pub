using UnityEngine;
using System.Collections;


public class DragMovement : MonoBehaviour
{
    int speed = 30;

    private void Awake()
    {
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -70, 60),
        //    Mathf.Clamp(transform.position.y, 30, 145), Mathf.Clamp(transform.position.z, -80, 50));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Transform>().position =
                new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z+.4f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Transform>().position =
                new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z - .4f);
        }
        MoveObject();

    }
    void MoveObject()
    {

        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);
        transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime * keyVertical, Space.World);
    }
}
