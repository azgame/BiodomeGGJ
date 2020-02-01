using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Components
    PlayerMove m_move;
    PlayerInput m_input;

    // Exposed


    // Internal
    Vector2 m_moveDir;


    // Start is called before the first frame update
    void Start()
    {
        m_moveDir = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        m_moveDir = m_input.GetInputXY();


    }
}
