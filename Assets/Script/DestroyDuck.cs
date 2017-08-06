using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyDuck : MonoBehaviour
{

    private void OnCollisionEnter2D()
    {
        if (GamePlay.FinishWave)
        {
            Destroy(gameObject);
            GamePlay.Duck--;
        }
        else if (gameObject.transform.position.y < -3.5f && gameObject.GetComponent<Rigidbody2D>().mass == 1.5f)
        {

            Destroy(gameObject);
            GamePlay.KilledBird++;
            GamePlay.Duck--;

        }
        else
        {
            int w;
            int h;
            if (gameObject.transform.position.x > 0)
                w = Random.Range(-100, -30);
            else
                w = Random.Range(30, 100);

            if (gameObject.transform.position.y > 0)
                h = Random.Range(-100, -30);
            else
                h = Random.Range(30, 100);

            gameObject.GetComponent<AudioSource>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = true;

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(w / 100f, h / 100f) * 200f * ((GamePlay.GameLevel / 4 + 1) * 0.5f));
            //GamePlay.Duck--;
        }
    }

    private void Update()
    {

        if (gameObject.GetComponent<Rigidbody2D>().mass == 1f)
        {
            // Debug.Log(RefreshTime);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1f)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

            if (gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.1f)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;

            if (GamePlay.FinishWave)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 40f));
            }
        }
    }
}
