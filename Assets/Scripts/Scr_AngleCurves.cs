using UnityEngine;

public class Scr_AngleCurves : MonoBehaviour
{
    public AnimationCurve rotationX;
    public AnimationCurve rotationY;
    public AnimationCurve rotationZ;
    public float factor = 1.0f;
    public float duration = 10.0f;

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        float time = ((int)(Time.time * 1000.0f) % (1000 * duration)) / 1000.0f;
        rotation.x = initialRotation.x + rotationX.Evaluate(time / duration) * factor;
        rotation.y = initialRotation.y + rotationY.Evaluate(time / duration) * factor;
        rotation.z = initialRotation.z + rotationZ.Evaluate(time / duration) * factor;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
