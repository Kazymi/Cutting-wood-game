using UnityEngine;

public class Vertical
{
    private int _id;
    private Vector3 _vector3;
    private Vector3 _motionVector;
    private Vector3 _positionObject;
    private float _magnitude;
    private MeshDeformer _deformer;
    public int ID => _id;
    public Vector3 Vector3 => _vector3;
    public float Magnitude => _magnitude;
    public Vertical(int id, Vector3 vector3, MeshDeformer deformer)
    {
        _deformer = deformer;
        _id = id;
        _vector3 = vector3;
        var vecrot3 = (vector3 - new Vector3(vector3.x, 0, 0));
        _motionVector = vecrot3.normalized;
        Debug.DrawLine(vector3, new Vector3(vector3.x, 0, 0), Color.blue, 1000);
        _magnitude = vecrot3.magnitude;
    }

    public void Deformation(float force, bool split)
    {
        if (_magnitude <= 0) return;
        var i = _motionVector * force * Time.deltaTime;
        Debug.Log(i);
        _magnitude -= i.magnitude;
        if (_magnitude <= 0)
        {
            if (split) _deformer.SplitMesh(_vector3.x);
            _vector3 = new Vector3(_vector3.x, 0, 0);
            return;
        }
        var correctMotionVector = _vector3 - i;
        _vector3 = correctMotionVector;
    }
}