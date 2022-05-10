using UnityEngine;

public class Scr_BarProperty : MonoBehaviour
{
    [SerializeField] private Transform bar = null;
    [SerializeField] private Vector3 barMinScale = Vector3.zero;
    [SerializeField] private Vector3 barMaxScale = Vector3.one;

    private float val = 1.0f;

    public float Value
    {
        get
        {
            return val;
        }
        set
        {
            val = Mathf.Clamp(value, 0.0f, 1.0f);
            if(bar) bar.localScale = Vector3.Lerp(barMinScale, barMaxScale, val);
        }
    }
}
