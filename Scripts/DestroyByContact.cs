using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public Transform deSpawn;
    public int scoreValue;
    private GameController gameController;
    public AudioClip goodAnswer;
    public AudioClip incorrectAnswer;

    void Start ()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }
    }

    void wrongAnswer()
    {
        GetComponent<AudioSource>().PlayOneShot(incorrectAnswer);
    }

    void rightAnswer()
    {
        GetComponent<AudioSource>().PlayOneShot(goodAnswer);
    }

    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if ( other.tag == "Boundary")
        {
            return;
        }

        Instantiate(explosion, deSpawn.position, deSpawn.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        if (other.tag == "Flat_Asteroid+MATH")
        {
            rightAnswer();
        }
        
        if (other.tag == "Flat_Asteroid+MATH fail")
        {
            wrongAnswer();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
