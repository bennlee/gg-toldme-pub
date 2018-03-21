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
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        

        pos.x = Mathf.Clamp(pos.x, -80 + 0.350f * pos.y, 70 - 0.375f * pos.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -80 - 0.085f * pos.y, 70 - 0.90f * pos.y);
        
        transform.position = pos;
    }
}
