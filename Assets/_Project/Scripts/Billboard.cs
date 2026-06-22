using UnityEngine;

// Makes this object always face the camera
public class Billboard : MonoBehaviour
{
    private Transform cam;
    
    private void Start()
    {
        cam = Camera.main.transform;
    }
    
    private void LateUpdate()
    {
        if (cam == null) return;
        transform.forward = cam.forward;
    }
}
