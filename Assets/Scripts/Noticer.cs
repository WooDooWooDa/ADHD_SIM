using DefaultNamespace;
using UnityEngine;

public class Noticer : MonoBehaviour
{
    public bool canDetect = false;
    public new Camera camera;
    public float detectionDistance = 12f;
    public float detectionRange = 1.2f;
    
    void Update()
    {
        if (!canDetect) return;
        
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        
        if (Physics.SphereCast(ray, detectionRange, out hit, detectionDistance))
        {
            Debug.DrawLine(camera.transform.position, hit.point, Color.red);
            if (hit.collider.CompareTag("TaskObject"))
            {
                hit.transform.gameObject.GetComponent<TaskObject>().TryNotice();
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(camera.transform.position, detectionRange);
        //Gizmos.DrawWireSphere(camera.transform.position + camera.transform.forward * detectionDistance, detectionRange);
        //Gizmos.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * detectionDistance);
    }
}
