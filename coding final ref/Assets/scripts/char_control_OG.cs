using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_RB : MonoBehaviour
{
    //Variables. Declare your variables.  
    public float forceAmt = 2f;

    private int i = 8;

    private Rigidbody2D rb;

    private Color defaultColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        //Find the Rigidbody on the object this script is on
        rb = GetComponent<Rigidbody2D>();

        //Find the gameManager 
        //gameManager = FindObjectsOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if statement to move right by using D
        if (Input.GetKey(KeyCode.D))
        {
            //Make Ridigbody move right
            rb.AddForce(Vector2.right * forceAmt);
        }
        //Make Ridigbody move left by using A
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * forceAmt);
        }
        //Make the Rigidbody move up by using W
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * forceAmt);
        }
        //Make the Rigidbody move down by using S
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * forceAmt);
        }
    }
    //OncollisionEnter2D happens when two 2D objects with colliders hit each other
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Make the cube change color when it hits  player 2 cube 
        if (collision.transform.tag == "Player 2")
        {
            //Find the color component of the item and change it to cyan
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        //Make the cube change color when it hits player 3 cube 
        if (collision.transform.tag == "Player 3")
        {
            //Find the color compnent of the item and change it to magenta 
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        GameManager.instance.StartGame();
    }

    //OnCollisionExit2D happens when two 2D objecs with Colliders seperate
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}