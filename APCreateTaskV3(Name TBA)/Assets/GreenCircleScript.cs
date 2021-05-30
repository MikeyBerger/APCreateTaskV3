using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCircleScript : MonoBehaviour
{
    private SpriteRenderer SR;
    private PlayerScript PS;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        SR.enabled = false;
        PS.Score++;
    }
}
