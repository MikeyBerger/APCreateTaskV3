using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform RedTarget; //The red circle
    public Transform GreenTarget; //The green circle
    public float Timer; //A timer that keeps counting up from 0
    public float Limit; //How long it takes to spawn a red or green circle
    private int RandTarget; //The index number of the list of circles
    public Transform []Targets; //The list of circles
    public Vector3 Center; //Where to draw the spawn area
    public Vector3 Size; //Size of the spawn area

    // Start is called before the first frame update
    void Start()
    {
        Targets[0] = GreenTarget; //The first object is the green circle
        Targets[1] = RedTarget; //The second object is the red circle
    }

    // Update is called once per frame
    void Update()
    {
        //The number of circles in the list is 2
        RandTarget = Random.Range(0, 2); 
        //Allows for random position spawning
        Vector3 Pos = Center + new Vector3(Random.Range(-Size.x, Size.x),Random.Range(-Size.y, Size.y), 0); 

        //If the value of the "Timer" variable exceeds the value of the "Limit" variable
        if (Timer >= Limit) 
        {
            //Spawn a circle from the list
            Instantiate(Targets[RandTarget], Pos, Quaternion.identity);
            //Set the variable "Timer" back to 0
            Timer = 0; 
        }

        //The variable "Timer" is equal to real-time
        Timer += Time.deltaTime; 

    }

    private void OnDrawGizmosSelected() //Draw a square
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); //Sets the color of the square to red
        Gizmos.DrawCube(Center, Size); //Determines the center and size of the square
    }
}
