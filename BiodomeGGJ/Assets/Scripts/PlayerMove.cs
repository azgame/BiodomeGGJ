using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    public  bool isFacingRight = true;
    public bool isFacingLeft;
    public Animator anim;

    // Components
    Rigidbody m_rb;

    // Exposed
    [Range(1, 20)]
    public float m_speed;

    [Range(1, 20)]
    public float m_dashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }


     void Update()
    {
        
    }

    public void Move(Vector2 moveDir_, float dash_)
    {
        Vector3 dash = new Vector3(moveDir_.x * dash_ * m_dashSpeed, 0.0f, moveDir_.y * dash_ * m_dashSpeed);
        Vector3 move = new Vector3(moveDir_.x * m_speed, 0.0f, moveDir_.y * m_speed);
        m_rb.velocity = move;
        m_rb.AddForce(dash, ForceMode.Impulse);
    }



    public void  Flip(){

        isFacingRight = !isFacingRight;

        Vector3 localscale = gameObject.transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;


    }

}
