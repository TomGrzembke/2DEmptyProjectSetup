using UnityEngine;

public class TopDownMovement : RBGetter
{
    [SerializeField] float speed = 5;
    [SerializeField] AnimationCurve moveCurve = AnimationCurve.Constant(0, 1, 1);
    [SerializeField] float neededFullSpeedWalkingTime = 1;
    [SerializeField] bool moveHelper;
    float walkingTime;
    float walkingSpeedPercentage;

    [Header("Debug")]
    [ShowOnly, SerializeField] Vector2 lastVelVal;


    protected override void AwakeInternal()
    {
    }

    void FixedUpdate()
    {
        Movement();

        MoveDebugValues();
    }

    void Movement()
    {
        if (InputManager.Instance.HasMoveInput)
        {
            walkingTime += Time.fixedDeltaTime;
            walkingSpeedPercentage = Mathf.Clamp01(walkingTime / neededFullSpeedWalkingTime);
        }
        else
            walkingTime = 0;

        if (InputManager.Instance.MoveStutter)
            walkingSpeedPercentage = 1;

        rb.velocity = InputManager.Instance.MovementVec * speed * moveCurve.Evaluate(walkingSpeedPercentage);

        if (!moveHelper)
            rb.velocity = InputManager.Instance.MovementVec * speed;
    }

    void MoveDebugValues()
    {
        if (rb.velocity.x != 0)
            lastVelVal.x = rb.velocity.x;

        if (rb.velocity.y != 0)
            lastVelVal.y = rb.velocity.y;
    }
}