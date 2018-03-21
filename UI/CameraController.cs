using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;

    public float scrollSpeed = 20f;
    public float minY = 30f;
    public float maxY = 165f;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 200f * Time.deltaTime;
        

        pos.x = Mathf.Clamp(pos.x, -110 + 0.350f * pos.y, 110 - 0.375f * pos.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -110 - 0.085f * pos.y, 110 - 0.90f * pos.y);
        
        transform.position = pos;
    }
}
