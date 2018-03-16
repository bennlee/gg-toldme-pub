using UnityEngine;
using System.Collections;



public class DragMovement : MonoBehaviour
{
    int speed = 30;

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Transform>().position =
                new Vector3(transform.position.x, transform.position.y - .3f, transform.position.z+.2f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Transform>().position =
                new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z - .2f);
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
