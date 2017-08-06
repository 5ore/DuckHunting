using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left
};

/// <summary>
/// SR - Koristi se za instanciranje i pomeranje objekta. -Lukaz
/// EN - Used in the main scene for instantiating and moving objects. -Lukaz
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cloud;
    [Header("Green duck")]
    [SerializeField]
    private GameObject right_green_duck;
    [SerializeField]
    private GameObject left_green_duck;
    [Header("Red duck")]
    [SerializeField]
    private GameObject right_red_duck;
    [SerializeField]
    private GameObject left_red_duck;

    [Header("Movement speed")]
    [SerializeField]
    private float right;
    [SerializeField]
    private float left;
    [SerializeField]
    private bool randomSpeed = false;

    // Boolean method used to turn the script on/off -Lukaz
    // Boolean koji ukljucuje/iskljucuje skriptu -Lukaz
    public static bool ON = true;

    void Start()
    {
        Invoke("_Start", 2f);
    }

    void _Start()
    {
        StartCoroutine(SpawnCloud());
        StartCoroutine(SpawnDuck());
    }

    IEnumerator SpawnDuck()
    {
        while (ON)
        {
            // Spawns next object in a random time between 1 and 3 seconds. -Lukaz
            // Instancira sledeci objekat nasumicno izmedju 1 i 3 sekundi. -Lukaz
            yield return new WaitForSeconds(Random.Range(1, 4));

            // Getting which side the next object will spawn to --- 1 is left, 2 is right -Lukaz
            // Dobijamo stranu na koju ce se sledeci objekat instancirati --- 1 levo, desno 2 -Lukaz
            if (Random.Range(1, 3) == 1)
            {
                // Get random height in which the object will spawn -Lukaz
                // Uzimamo nasumicnu visinu na kojoj ce se ob stvoriti -Lukaz
                float y = Random.Range(0, Screen.height - 3);
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(-3, y, 10));

                if (GameObject.Find("Environment") == null)
                    yield break;

                // Instantiating the object -Lukaz
                // Stvaramo objekat -Lukaz
                GameObject newDuck = (Random.Range(0, 2) == 0) ?
                    // GREEN RIGHT DUCK
                    Instantiate(right_green_duck, position, new Quaternion(0, 0, 0, 0),
                    GameObject.Find("Environment").transform)
                    :
                    // RED RIGHT DUCK
                    Instantiate(right_red_duck, position, new Quaternion(0, 0, 0, 0),
                    GameObject.Find("Environment").transform);

                if (randomSpeed)
                    StartCoroutine(MoveObject(newDuck, Direction.Right, Random.Range(3, 10)));
                else
                    StartCoroutine(MoveObject(newDuck, Direction.Right, right));
            }
            else
            {
                // Get random height in which the cloud will spawn -Lukaz
                // Uzimamo nasumicnu visinu na kojoj ce se oblak stvoriti -Lukaz
                float y = Random.Range(2, Screen.height - 3);
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 3, y, 10));

                if (GameObject.Find("Environment") == null)
                    yield break;

                // Instantiating the object -Lukaz
                // Stvaramo objekat -Lukaz
                GameObject newDuck = (Random.Range(0, 2) == 0) ?
                    // GREEN LEFT DUCK
                    Instantiate(left_green_duck, position, new Quaternion(0, 0, 0, 0),
                    GameObject.Find("Environment").transform)
                    :
                    // RED LEFT DUCK
                    Instantiate(left_red_duck, position, new Quaternion(0, 0, 0, 0),
                    GameObject.Find("Environment").transform);
                if (randomSpeed)
                    StartCoroutine(MoveObject(newDuck, Direction.Left, Random.Range(3, 10)));
                else
                    StartCoroutine(MoveObject(newDuck, Direction.Left, left));
            }

            yield return null;
        }
    }

    /// <summary>
    /// Used for spawning the object -Lukaz
    /// Koristi se za instanciranje objekta -Lukaz
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnCloud()
    {
        while (ON)
        {
            // Spawns next object in a random time between 1 and 5 seconds. -Lukaz
            // Instancira sledeci objekat nasumicno izmedju 1 i 5 sekundi. -Lukaz
            yield return new WaitForSeconds(Random.Range(1, 6));

            // Getting which side the next object will spawn to --- 1 is left, 2 is right -Lukaz
            // Dobijamo stranu na koju ce se sledeci objekat instancirati --- 1 levo, desno 2 -Lukaz
            if (Random.Range(1, 3) == 1)
            {
                // Get random height in which the object will spawn -Lukaz
                // Uzimamo nasumicnu visinu na kojoj ce se ob stvoriti -Lukaz
                float y = Random.Range(0, Screen.height - 3);
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(-3, y, 10));

                if (GameObject.Find("Environment") == null)
                    yield break;

                // Instantiating the object -Lukaz
                // Stvaramo objekat -Lukaz
                GameObject newCloud = Instantiate(cloud, position, new Quaternion(0, 0, 0, 0),
                    GameObject.Find("Environment").transform);
                if (randomSpeed)
                    StartCoroutine(MoveObject(newCloud, Direction.Right, Random.Range(2, 6)));
                else
                    StartCoroutine(MoveObject(newCloud, Direction.Right, right));
            }
            else
            {
                // Get random height in which the cloud will spawn -Lukaz
                // Uzimamo nasumicnu visinu na kojoj ce se oblak stvoriti -Lukaz
                float y = Random.Range(2, Screen.height - 3);
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 3, y, 10));

                if (GameObject.Find("Environment") == null)
                    yield break;

                // Instantiating the object -Lukaz
                // Stvaramo objekat -Lukaz
                GameObject newCloud = Instantiate(cloud, position, new Quaternion(0, 0, 0, 0),
                   GameObject.Find("Environment").transform);
                if (randomSpeed)
                    StartCoroutine(MoveObject(newCloud, Direction.Left, Random.Range(2, 6)));
                else
                    StartCoroutine(MoveObject(newCloud, Direction.Left, left));
            }

            yield return null;
        }

    }
    /// <summary>
    /// Moves the clouds. -Lukaz
    /// Pomera oblake. -Lukaz
    /// </summary>
    /// <param name="cloud"></param>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    IEnumerator MoveObject(GameObject _object, Direction direction, float speed)
    {
        // Adding rigidbody component to cloud object if it does not have one already -Lukaz
        // Dodajemo rigidbody komponentu na oblak ukoliko vec ne postoji -Lukaz
        if (_object.GetComponent<Rigidbody2D>() == null)
            _object.AddComponent<Rigidbody2D>();
        if (_object.GetComponent<Collider2D>() == null)
            _object.AddComponent<BoxCollider2D>();

        _object.GetComponent<Collider2D>().isTrigger = true;

        Rigidbody2D rigidbody = _object.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;

        while (rigidbody != null)
        {
            // Moving the cloud relative to direction -Lukaz
            // Pomeramo oblak u zavisnosti od smera -Lukaz
            switch (direction)
            {
                case Direction.Right:
                    rigidbody.velocity = Vector2.right * speed;
                    break;
                case Direction.Left:
                    rigidbody.velocity = Vector2.left * speed;
                    break;
            }
            yield return null;
        }
    }
}
