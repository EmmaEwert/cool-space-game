using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearAsteroid : MonoBehaviour
{
    public float speed;
    private Vector3 startPoint;
    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(startPoint * -speed * Time.deltaTime);
        if (Vector3.Dot(startPoint, transform.position) < 0){
            obstacle.SetActive(false);
        }
    }

    public void OnNewLife(){
        transform.position = startPoint;
        obstacle.SetActive(true);
    }
}
