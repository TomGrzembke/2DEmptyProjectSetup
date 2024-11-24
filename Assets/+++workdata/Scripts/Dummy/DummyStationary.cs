using UnityEngine;

public class DummyStationary : RBGetter
{
    [SerializeField] Transform dummyOrigTrans;
    [SerializeField] float speed = 10;
    [SerializeField] float stoppingDist = 5;

    protected override void AwakeInternal()
    {
    }

    void FixedUpdate()
    {
        LerpResetPos();

    }

    void LerpResetPos()
    {
        if (stoppingDist > Vector3.Distance(transform.position, dummyOrigTrans.position)) return;

        rb.AddForce((dummyOrigTrans.position.normalized - transform.position.normalized) * speed, ForceMode2D.Force);
    }
}