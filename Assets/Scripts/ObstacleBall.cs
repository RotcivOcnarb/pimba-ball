using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleBall : MonoBehaviour
{
    public Color color;
    [Range(1, 10)]
    public int hits;
    public int currentHits;
    int rounds = 0;

    GameObject textObject;
    TextMesh textMesh;

    Animator animator;
    MeshRenderer meshRenderer;

    public PimbaBall pimbaBall;

    public float scaleAnimation;
    public float obstacleSize;

    Color green;
    Color red;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        textObject = transform.Find("Text").gameObject;
        textMesh = textObject.GetComponent<TextMesh>();

        currentHits = hits;

        green = new Color(136/255f, 249/255f, 68/255f, 1);
        red = new Color(228 / 255f, 64 / 255f, 22 / 255f, 1);

        animator = GetComponent<Animator>();
    }

    float DeltaFactor() {
        return 60 / (1 / Time.deltaTime);
    }
   
    void Update()
    {

        bool dead = rounds >= 6 || currentHits <= 0;
        animator.SetBool("Dead", dead);

        color = Color.Lerp(green, red, rounds / 5f);
        meshRenderer.material.color += (color - meshRenderer.material.color) / 5f * DeltaFactor();
        
        textMesh.text = "" + currentHits;
        transform.localScale = new Vector3(scaleAnimation * obstacleSize, scaleAnimation * obstacleSize, 1);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Persist()
    {
        rounds++;
        if(rounds == 6)
        {
            float dmg = 10 * GlobalVars.getPlayerProfile().GetPlayerDeffense();
            Debug.Log("Player damage received: " + dmg);
            pimbaBall.Damage(dmg);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player")) {
            int dmg = (int)GlobalVars.getPlayerProfile().GetHitDamageMultiplier();;
            Debug.Log("Ball collided, damage received: " + dmg);
            currentHits -= dmg;
            animator.SetBool("Hit", true);
        }
    }

    public void DisableHit()
    {
        animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pimbaBall.TriggerOnObstacle(this);
        }
    }
}
