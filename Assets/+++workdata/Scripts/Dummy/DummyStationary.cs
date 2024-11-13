using UnityEngine;

public class DummyStationary : RBGetter
{
    [SerializeField] Transform dummyOriginalPos;
    [SerializeField] float divisionAmount = 10;

    protected override void AwakeInternal()
    {
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, dummyOriginalPos.localPosition, Mathf.Clamp01(Vector3.Distance(transform.localPosition, dummyOriginalPos.localPosition) / divisionAmount));
    }
}