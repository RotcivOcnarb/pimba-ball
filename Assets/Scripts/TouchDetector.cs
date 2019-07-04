using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{

    Vector2 startPoint;
    Vector2 endPoint;
    bool touching = false;

    LineRenderer lineRenderer;

    public PimbaBall pimbaBall;
    Rigidbody2D body;
    public GameManager gameManager;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        body = pimbaBall.GetComponent<Rigidbody2D>();
    }

    void TouchBegin(Vector2 position)
    {
        startPoint = Camera.main.ScreenToWorldPoint(position);
        endPoint = Camera.main.ScreenToWorldPoint(position);
        touching = true;
    }

    void TouchMove(Vector2 position)
    {
        endPoint = Camera.main.ScreenToWorldPoint(position);
    }

    void TouchEnd()
    {
        //Dispara a bolinha
        touching = false;
        gameManager.TouchEnded((startPoint - endPoint));
    }

    // Update is called once per frame
    void Update()
    {

        //Draws the LineRenderer only if the ball is stopped and the player is dragging
        if (touching && body.velocity.magnitude < 0.1)
        {
            Color c = lineRenderer.startColor;
            lineRenderer.startColor = new Color(c.r, c.g, c.b, 1);
            c = lineRenderer.endColor;
            lineRenderer.endColor = new Color(c.r, c.g, c.b, 1);
        }
        else
        {
            Color c = lineRenderer.startColor;
            lineRenderer.startColor = new Color(c.r, c.g, c.b, 0);
            c = lineRenderer.endColor;
            lineRenderer.endColor = new Color(c.r, c.g, c.b, 0);
        }

        //Sets the LineRenderer position to the drag line
        Vector3 mag = endPoint - startPoint;
        lineRenderer.SetPosition(0, pimbaBall.transform.position);
        lineRenderer.SetPosition(1, pimbaBall.transform.position - mag);

        //Handles Mouse input
        if (Input.GetMouseButtonDown(0))
            TouchBegin(Input.mousePosition);
        if (Input.GetMouseButton(0))
            TouchMove(Input.mousePosition);
        if(Input.GetMouseButtonUp(0))
            TouchEnd();

        //Handles touch input (device)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
                TouchBegin(touch.position);
            else if (touch.phase == TouchPhase.Moved)
                TouchMove(touch.position);
            else if (touch.phase == TouchPhase.Ended)
                TouchEnd();
        }

    }
}
