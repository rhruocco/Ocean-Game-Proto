using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mount : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    bool followplayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        followplayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followplayer)
        {
            this.transform.position = player.transform.position;
            
        }
        Debug.Log(followplayer);
    }

    public void followPlayer(bool yesOrNo)
    {
        followplayer = yesOrNo;

        this.GetComponent<BoxCollider2D>().enabled = !yesOrNo;

        transform.position = new Vector2(player.transform.position.x + 1, player.transform.position.y - 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && followplayer)
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());
            Debug.Log("dadada");
        }
    }
}
