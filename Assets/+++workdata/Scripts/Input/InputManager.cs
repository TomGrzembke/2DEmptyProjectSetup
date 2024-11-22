using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    PlayerInputActions input;
    Camera cam;

    [field: SerializeField] public Vector2 MousePos { get; private set; } 
    [field: SerializeField] public Vector2 MovementVec { get; private set; } 
    [field: SerializeField] public bool MoveStutter { get; private set; }   

    public InputAction moveAction
    {
        get;
        private set;
    }

    public InputAction stutterMoveAction
    {
        get;
        private set;
    }

    public bool HasMoveInput => MovementVec.magnitude > 0;

    public InputAction leftclickAction
    {
        get;
        private set;
    }

    public InputAction rightClickAction
    {
        get;
        private set;
    }

    void Awake()
    {
        Instance = this;
        input = new();

        moveAction = input.Player.Move;
        moveAction.performed += ctx => Movement(ctx.ReadValue<Vector2>().normalized);
        moveAction.canceled += ctx => Movement(ctx.ReadValue<Vector2>().normalized);

        stutterMoveAction = input.Player.StutterMove;
        stutterMoveAction.performed += ctx => MovementStutter(true);
        stutterMoveAction.canceled += ctx => MovementStutter(false);

        leftclickAction = input.Player.LeftClick;
        rightClickAction = input.Player.RightClick;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Movement(Vector2 direction) => MovementVec = direction;
    void MovementStutter(bool stutter) => MoveStutter = stutter;

    void Update()
    {
        if (!cam) 
            cam = Camera.main;

        if (cam)
            MousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    /// <summary> Takes a Method and an Inputaction to subscribe them</summary>
    /// <param name="method"></param>
    public void SubscribeTo(Action<InputAction.CallbackContext> method, InputAction inputAction)
    {
        inputAction.performed += ctx => method(ctx);
        inputAction.canceled += ctx => method(ctx);
    }

    #region OnEnable/Disable
    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
    #endregion
}
