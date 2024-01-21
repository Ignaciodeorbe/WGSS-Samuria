using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;

    [SerializeField]
    public float parallaxValue;
    [SerializeField]
    public float parallaxScale;

    float distance = 0;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        distance += (parallaxScale * parallaxValue);
     
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(distance > length)
        {           
            distance -= length;
        }
    }
}
