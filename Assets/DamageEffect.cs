using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{

    public Vector2 velocity;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        //movimenta baseado na velocidade
        Vector3 pos = transform.position;
        pos += new Vector3(velocity.x, velocity.y, 0) * 0.1f;
        transform.position = pos;

        //Calcula a direção objetivo
        Vector2 direction = (manager.damageTarget.transform.position - transform.position);
        direction.Normalize();
        //Debug.Log("Direction: " + direction + ", Velocity: " + velocity);
        
        float angle_diff = Vector2.Angle(velocity, direction); //pega o angulo entre os dois

        //Eu nao sei se eu devo jogar pra direita ou pra esquerda, então eu testo os dois
        //e vejo em qual diminuiu a diferença
        float factor = 0.05f;
        velocity = RotateVector2(velocity, angle_diff * factor); //rotaciona um pouco pra um lado
        float a1 = Vector2.Angle(velocity, direction); //calcula o novo angulo
    
        if(a1 > angle_diff){// o angulo aumentou, gira pro outro lado
            velocity = RotateVector2(velocity, -2*angle_diff*factor);
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));

    }

    public void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(velocity.x, velocity.y, 0));
        Vector2 direction = (manager.damageTarget.transform.position - transform.position);
        direction.Normalize();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0));
    }

    Vector2 RotateVector2(Vector2 v, float degrees){
        float radians = degrees * Mathf.Deg2Rad;
         float sin = Mathf.Sin(radians);
         float cos = Mathf.Cos(radians);
         
         float tx = v.x;
         float ty = v.y;

         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);

        return v;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject == manager.damageTarget){
            //Destroi eu, treme a camera, coloca efeito vermelho na tela e dá dano no player
            manager.mainCamera.transform.position = manager.mainCamera.transform.position + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .2f), 0);
            manager.damageUI.color = new Color(1, 1, 1, 1);
            manager.pimbaBall.Damage();
            Destroy(gameObject);
        }
    }
}
