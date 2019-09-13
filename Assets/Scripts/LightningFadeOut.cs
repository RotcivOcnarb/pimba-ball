using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFadeOut : MonoBehaviour
{
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        RecalculateLines(
            GetComponent<LineRenderer>().GetPosition(0),
            GetComponent<LineRenderer>().GetPosition(GetComponent<LineRenderer>().positionCount-1)
        );
        
        Color c = GetComponent<LineRenderer>().startColor;
        c.a *= 0.9f;
        GetComponent<LineRenderer>().startColor = c;
        GetComponent<LineRenderer>().endColor = c;

        if(timer > 2){
            Destroy(gameObject);
        }
        
    }

    public void RecalculateLines(Vector3 lStart, Vector3 lEnd){
        LineRenderer line = GetComponent<LineRenderer>();
        float dist = (lEnd - lStart).magnitude;
        int points = (int)(dist * 5);
        if(points < 2)
            points = 2;

        Vector3[] newList = new Vector3[points];
        for(int i = 0; i < points; i ++){
            newList[i] = Vector3.Lerp(lStart, lEnd, i / (float)(points-1));
        }
        for(int i = 1; i < points - 1; i++){
            newList[i] += Random.insideUnitSphere * 0.2f;
        }
        line.positionCount = points;
        line.SetPositions(newList);
    }
}
