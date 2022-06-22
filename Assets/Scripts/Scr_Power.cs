using UnityEngine;

public class Scr_Power : Scr_BarProperty
{
    [Space]
    [SerializeField] private float regenerationRate = 0.5f;

    public void Update()
    {
        Value += regenerationRate * Time.unscaledDeltaTime;
    }
}
