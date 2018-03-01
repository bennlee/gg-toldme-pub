using UnityEngine;
using System.Collections;

namespace TVNT {
    public class EndlessCamera : MonoBehaviour {

        public Transform cameraTarget = null;
        public Vector3 targetOffset;
        public float cameraRecenterRate;
        public float cameraSpeed;
        public bool flipDirection = true;
        public bool activated = false;
        public float treadmillDistance = 2000;

        public float playerOutOfBoundXMin = 0f;
        public float playerOutOfBoundXMax = 1f;
        public float playerOutOfBoundYMin = 0f;
        public float playerOutOfBoundYMax = 1f;

        Transform tracker = null;
        Vector3 velocity = Vector3.zero;

        void Start() {
            InitializeCamera();
        }

        public void InitializeCamera()
        {
            if (cameraTarget)
            {
                if (!tracker)
                {
                    tracker = new GameObject("CameraTracker").transform;
                }
                tracker.position = cameraTarget.position;
            }
        }

        void LateUpdate() {
            if (activated && cameraTarget)
            {
                if (!flipDirection) {
                    float cameraMovementSpeed = cameraSpeed;
                    
                    if (cameraTarget.position.z > tracker.position.z)
                    {
                        cameraMovementSpeed += (tracker.position.z - cameraTarget.position.z);
                    }
                    tracker.Translate(0, 0, cameraMovementSpeed * Time.deltaTime);
                } else
                {
                    float cameraMovementSpeed = -cameraSpeed;

                    if (cameraTarget.position.z < tracker.position.z)
                    {
                        cameraMovementSpeed += -(tracker.position.z - cameraTarget.position.z);
                    }
                    tracker.Translate(0, 0, cameraMovementSpeed * Time.deltaTime);
                }

                Vector2 targetCameraPosition = Camera.main.WorldToViewportPoint(cameraTarget.position + new Vector3(0, 0, 0.5f * (flipDirection ? -1 : 1) * PatternSettings.tiledSize)) * (flipDirection ? -1 : 1);
                if (targetCameraPosition.x < (flipDirection ? -playerOutOfBoundXMax : playerOutOfBoundXMin) || targetCameraPosition.y < (flipDirection ? -playerOutOfBoundYMax : playerOutOfBoundYMin))
                {
                    TVNTManager.instance.PlayerDead();
                }

                Vector3 camPosition = new Vector3(tracker.position.x, transform.position.y, tracker.position.z) + targetOffset;
                transform.position = Vector3.SmoothDamp(transform.position, camPosition,ref velocity, cameraRecenterRate * Time.deltaTime);

                if (cameraTarget.position.z * (flipDirection?-1:1) > treadmillDistance)
                {
                    TVNTManager.instance.CameraTreadmill();
                }
            }
        }

        public void ResetByAmount(float resetAmount)
        {
            Camera.main.transform.position += new Vector3(0, 0, resetAmount);
            tracker.position += new Vector3(0, 0, resetAmount);
        }
    }
}
