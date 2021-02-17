using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public Transform RedTarget;
    //public Transform GreenTarget;
    public float Timer;
    public float Limit;
    private int RandTarget;
    public Transform []Targets;
    public Vector3 Center; //Where to draw the spawn area
    public Vector3 Size; //Size of the spawn area

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RandTarget = Random.Range(0, 2);
        Vector3 Pos = Center + new Vector3(Random.Range(-Size.x, Size.x), Random.Range(-Size.y, Size.y), 0); //Allows for random position spawning
        

        if (Timer >= Limit)
        {
            Instantiate(Targets[RandTarget], Pos, Quaternion.identity);
            Timer = 0;
        }

        Timer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(Center, Size);
    }
}
