using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myRB;
    bool grounded, mounted;

    [SerializeField]
    Mount mountScript;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        grounded = true;
        mounted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mounted)
        {
            StandardMovement();
        }
        else
        {
            MantaMovement();
        }

        //Debug.Log(mounted);
        //Debug.Log(grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("mount"))
        {
            mounted = true;
            mountScript.followPlayer(true);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = false;
        }
    }

    void StandardMovement()
    {
        float horizontalDir = Input.GetAxisRaw("Horizontal");

        myRB.velocity = new Vector2(horizontalDir * 7f, myRB.velocity.y);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 7f);
        }
    }

    void MantaMovement()
    {
        myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 10, Input.GetAxisRaw("Vertical") * 10);
        if (Input.GetButton("Fire3"))
        {
            mounted = false;
            mountScript.followPlayer(false);
        }
    }
}
