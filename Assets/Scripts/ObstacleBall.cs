using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;
using System.Linq;
public class ObstacleBall : MonoBehaviour
{
    public Color color;
    [Range(1, 10)]
    public int hits;
    public int currentHits;
    int rounds = 0;

    public GameObject lightningPrefab;

    GameObject textObject;
    TextMesh textMesh;

    Animator animator;
    MeshRenderer meshRenderer;

    public PimbaBall pimbaBall;

    public GameManager manager;
    public GameObject coinEffect;
    public GameObject explosionEffect;

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

    public void Damage(int damage){

        currentHits -= damage;
        if(animator != null){
            animator.SetBool("Hit", true);
        }

        CoinEffect ef = Instantiate(coinEffect).GetComponent<CoinEffect>();
        ef.transform.position = gameObject.transform.position;
        int coins = GlobalVars.getPlayerProfile().GetPlayerCoinMultiplier();

        ef.value = coins;
        GlobalVars.getPlayerProfile().coins += coins;

        if(manager != null)
        manager.AddScore(); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player")) {
            // Player deu dano na bola
            int dmg = (int)GlobalVars.getPlayerProfile().GetHitDamageMultiplier();

            Damage(dmg);

            //Bomba
            Vector2 bomb = GlobalVars.getPlayerProfile().GetBombValue();
            float rnd = Random.Range(0, 1f);
            if(rnd < bomb.x){
                //Itera por todos os obstaculos q não são ele, e checa quais q estão perto
                ObstacleBall[] list = Resources.FindObjectsOfTypeAll(typeof(ObstacleBall)) as ObstacleBall[];
                foreach(ObstacleBall ob in list){
                    if(ob == this) continue;
                    if((ob.transform.position - transform.position).magnitude < 1.5f){
                        ob.Damage((int)bomb.y);
                    }
                }
                Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
            }

            ChainReaction(this, GlobalVars.getPlayerProfile().GetChainReactionValue());
            
        }
    }

    public void ChainReaction(ObstacleBall obs, Vector2 chain){
        //Reação em cadeia

            for(int i = 0; i < 3; i ++){
                if(Random.Range(0, 1f) < chain.x){

                    //Calcula o target random
                    ObstacleBall[] list = Resources.FindObjectsOfTypeAll(typeof(ObstacleBall)) as ObstacleBall[];
                    list = list.Select(b => b).Where(b => b.gameObject.activeInHierarchy).ToArray();
                    
                    if(list.Length == 1) return;
                    
                    ObstacleBall rand_ob = list[Random.Range(0, list.Length)];
                    while(rand_ob == obs || !rand_ob.gameObject.activeInHierarchy){// impede de acertar eu mesmo
                        rand_ob = list[Random.Range(0, list.Length)];
                    }

                    //Cria o objeto de linha
                    GameObject line = Instantiate(lightningPrefab);

                    line.GetComponent<LightningFadeOut>().RecalculateLines(
                        obs.transform.position,
                        rand_ob.transform.position
                    );

                    //Dá dano e propaga o efeito
                    rand_ob.Damage((int)chain.y);
                    ChainReaction(rand_ob, chain * new Vector2(.5f, 1));

                }
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
