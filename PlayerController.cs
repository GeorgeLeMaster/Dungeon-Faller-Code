using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public bool jumped;

    public Rigidbody2D rb;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        jumped = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (jumped == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Jump");
        }

        if (jumped == false)
        {

            rb.AddRelativeForce(Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
            rb.drag = 5;
        }

    }


    IEnumerator Jump()
    {
        rb.drag = 0.5f;
        rb.gravityScale = 1.5f;
        rb.AddRelativeForce(Vector2.up * 300);
        jumped = true;
        yield return new WaitForEndOfFrame();
        FindObjectOfType<AudioManager>().Play("Jump");


    }

}
