using DefaultNamespace;
using UnityEngine;

public class Noticer : MonoBehaviour
{
    public LayerMask taskLayer;
    public new Camera camera;
    public float detectionDistance = 10f;
    public float detectionRange = 1f;
    
    void Update()
    {
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        
        if (Physics.SphereCast(ray, detectionRange, out hit, detectionDistance))
        {
            print(hit.collider.gameObject.name);
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
