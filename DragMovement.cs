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
