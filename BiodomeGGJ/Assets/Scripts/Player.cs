using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    // Components
    PlayerMove m_move;
    PlayerInputActions m_inputActions;

    // Internal
    Vector2 m_moveDir;


    // Start is called before the first frame update
    void Start()
    {
        m_moveDir = Vector2.zero;
        m_move = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        m_move.Move(m_moveDir);
    }

    // Move Action
    public void Move(InputAction.CallbackContext context)
    {
        m_moveDir = context.ReadValue<Vector2>();
    }
}