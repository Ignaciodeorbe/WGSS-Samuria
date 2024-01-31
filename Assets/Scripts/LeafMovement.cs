using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMovement : MonoBehaviour
{
    int randRotation;
    private void Start()
    {
        randRotation = Random.Range(3, 5);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 25 * randRotation, 0) * Time.deltaTime);
    }
}
