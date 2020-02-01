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
        // Input
        m_move.Move(m_moveDir);
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_moveDir = context.ReadValue<Vector2>();
    }
}



//Vector2 GetInputXY(int actorId_)
//{
//    if (m_gamepad != null)
//    {
//        return m_gamepad.leftStick.ReadValue();
//    }
//    else
//    {
//        Vector2 moveDir = new Vector2();
//        switch (actorId_)
//        {
//            case 1:

//                float horizontal = m_keyboard.dKey.ReadValue() - m_keyboard.aKey.ReadValue();
//                float vertical = m_keyboard.wKey.ReadValue() - m_keyboard.sKey.ReadValue();
//                moveDir = new Vector2(horizontal, vertical);
//                break;
//            case 2:
//                break;
//            case 3:
//                break;
//            case 4:
//                break;
//            default:
//                break;
//        }

//        return moveDir;
//    }
//}