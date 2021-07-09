using UnityEngine;

public class Vertex
{
    private int _id;
    private Vector3 _vector3;
    private Vector3 _motionVector;
    private Vector3 _positionObject;
    public int ID => _id;
    public Vector3 Vector3 => _vector3;
    public Vertex(int id, Vector3 vector3)
    {
        _id = id;
        _vector3 = vector3;
        var vecrot3 = (vector3 - new Vector3(vector3.x, 0, 0));
        _motionVector = vecrot3.normalized;
        Debug.DrawLine(vector3, new Vector3(vector3.x, 0, 0), Color.blue, 1000);
    }

    public void Deformation(float force)
    {
        if (force <= 0)
        {
            _vector3 = new Vector3(_vector3.x, 0, 0);
            return;
        }
        var i = _motionVector * force;
        var correctMotionVector =_vector3 - i;
        _vector3 = correctMotionVector;
    }
}