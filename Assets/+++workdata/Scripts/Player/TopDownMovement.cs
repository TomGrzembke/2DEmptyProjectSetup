using UnityEngine;

public class TopDownMovement : RBGetter
{
    [SerializeField] float speed = 5;
    [SerializeField] AnimationCurve moveCurve = AnimationCurve.Constant(0, 1, 1);
    [SerializeField] float neededFullSpeedWalkingTime = 1;
    float walkingTime;
    float walkingSpeedPercentage;

    protected override void AwakeInternal()
    {
    }

    void Update()
    {
        if (InputManager.Instance.MovementVec != Vector2.zero)
        {
            walkingTime += Time.fixedDeltaTime;
            walkingSpeedPercentage = Mathf.Clamp01(walkingTime / neededFullSpeedWalkingTime);
        }
        else
            walkingTime = 0;

        rb.velocity = InputManager.Instance.MovementVec * speed * moveCurve.Evaluate(walkingSpeedPercentage);
    }
}