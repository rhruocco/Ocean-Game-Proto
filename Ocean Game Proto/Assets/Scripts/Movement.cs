using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myRB;
    bool grounded, mounted;

    float horizontalDir;

    string whatAmIRiding;

    [SerializeField]
    Mount mountScript;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        grounded = true;
        mounted = false;

        horizontalDir = Input.GetAxisRaw("Horizontal");

    }

    // Update is called once per frame
    void Update()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");

        if (!mounted)
        {
            StandardMovement();
        }
        else
        {
            if (whatAmIRiding.Equals("manta"))
            {
                MantaMovement();
            }
            if (whatAmIRiding.Equals("crab"))
            {
                CrabMovement();
            }

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
        if (collision.gameObject.CompareTag("manta"))
        {
            mounted = true;
            whatAmIRiding = "manta";

            mountScript = collision.gameObject.GetComponent<Mount>();
            mountScript.followPlayer(true);
        }

        if (collision.gameObject.CompareTag("crab"))
        {
            mounted = true;
            whatAmIRiding = "crab";

            mountScript = collision.gameObject.GetComponent<Mount>();
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
        myRB.velocity = new Vector2(horizontalDir * 7f, myRB.velocity.y);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 7f);
        }
    }

    void MantaMovement()
    {
        myRB.velocity = new Vector2(horizontalDir * 10, Input.GetAxisRaw("Vertical") * 10);
        if (Input.GetButton("Fire3"))
        {
            mounted = false;
            mountScript.followPlayer(false);
        }
        if (Input.GetButton("Fire"))
        {

        }
    }

    void CrabMovement()
    {

        myRB.velocity = new Vector2(horizontalDir * 5f, myRB.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 2f);
            }
            else
            {

            }
        }
    }
}
