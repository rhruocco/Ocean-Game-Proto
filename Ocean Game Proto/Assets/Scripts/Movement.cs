using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myRB;

    [SerializeField]
    GameObject bullet;

    bool grounded, mounted;

    float horizontalDir, vertDir;

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
        GetAxes();

        DismountCheck();

        if (!mounted)
        {
            StandardMovement();
        }
        else
        {
            switch(whatAmIRiding)
            {
                case "crab":
                    CrabMovement();
                    break;
                case "manta":
                    MantaMovement();
                    break;
                default:
                    break;

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
        if (collision.gameObject.CompareTag("hazard"))
        {
            Die();
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
        myRB.velocity = new Vector2(horizontalDir * 10f, vertDir * 10f);
        if (Input.GetButtonDown("Jump"))
        {
             Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    void CrabMovement()
    {
        if (!grounded)
        {
            vertDir = 0;
            myRB.AddForce(new Vector2(0, -20f)); 
        }
        myRB.velocity = new Vector2(horizontalDir * 5f, vertDir * 10f);
    }

    void DismountCheck()
    {
        if (Input.GetButton("Fire3"))
        {
            mounted = false;
            mountScript.followPlayer(false);
        }
    }

    void GetAxes()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");
        vertDir = Input.GetAxisRaw("Vertical");
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
