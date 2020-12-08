using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float bgSpeed = 0.1f;
    public float TimeForScroll = 0;
    public Renderer bgRend;

    public CharacterController2D player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController2D>();
        // playerMoving = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            // Debug.Log("right");
            TimeForScroll += Time.deltaTime;
            bgSpeed = 0.1f;

        }
        if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            //Debug.Log("left");
            TimeForScroll -= Time.deltaTime;
            bgSpeed = 0.1f;

        }

        Vector2 offset = new Vector2(TimeForScroll * bgSpeed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
