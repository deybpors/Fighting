using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class CustomInput : MonoBehaviour
{
    public Vector2 MoveValue { get; private set; }
    public bool JumpValue { get; private set; }
    public bool HitValue { get; private set; }

    public Action OnJump;
    public Action OnHit;

    private InputActions inputActions;
    private InputAction move;
    private InputAction hit;
    private InputAction jump;
    private PhotonView photonView;

    private void Awake()
    {
        inputActions = new InputActions();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        MoveValue = move.ReadValue<Vector2>();

        if (jump.WasPressedThisFrame())
        {
            OnJump?.Invoke();
            JumpValue = true;
        }
        if(jump.WasReleasedThisFrame())
        {
            JumpValue = false;
        }

        if (hit.WasPressedThisFrame())
        {
            OnHit?.Invoke();
            HitValue = true;
        }
        if (hit.WasReleasedThisFrame())
        {
            HitValue = false;
        }
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();
        hit = inputActions.Player.Hit;
        hit.Enable();
        jump = inputActions.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        hit.Disable();
        jump.Disable();
    }

    public PhotonView GetPhotonView()
    {
        return photonView;
    }
}
