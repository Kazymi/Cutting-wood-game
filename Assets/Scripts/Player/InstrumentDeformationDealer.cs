using UnityEngine;

public class InstrumentDeformationDealer : MonoBehaviour
{
    [SerializeField] private LayerMask ingoreLayer;
    [SerializeField] private float force;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.up,out hit, 2f,ingoreLayer)) {
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            Debug.Log(hit.collider.name);
            if (deformer) {
                Vector3 point = hit.point;
                point += hit.normal;
                deformer.AddDeformingForce(point, force);
            }
        }   
    }
}
