using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public List<MovementStep> movePattern;
    public bool moveInLoop;

    private int stepIndex;
    private float stepProgress;

    private bool movingForward;

    [System.Serializable]
    public struct MovementStep
    {
        public Vector3 eulerAngle;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IterateMovement();
    }

    private void IterateMovement(){

        int nextPoint = movingForward ? stepIndex + 1 : stepIndex - 1;

        if(nextPoint == -1) {
            nextPoint = 1;
            movingForward = true;
        } else if (nextPoint >= movePattern.Count){
            nextPoint = moveInLoop ? 0 : stepIndex - 1;
        }

        // Now, lerp between 2 points at some speed
        Vector3 startPoint = movePattern[stepIndex].eulerAngle;
        Vector3 endPoint = movePattern[nextPoint].eulerAngle;
        float distance = (endPoint - startPoint).magnitude;

        Vector3 pointAlongPath = Vector3.Lerp(startPoint, endPoint, stepProgress);

        //update rotation
        if(true){
            this.transform.rotation = Quaternion.Euler(pointAlongPath);
            stepProgress += Time.deltaTime * speed / distance;
            if(stepProgress > 1){
                stepProgress = 0;
                stepIndex = nextPoint;
            }
        }

    }


    

}
