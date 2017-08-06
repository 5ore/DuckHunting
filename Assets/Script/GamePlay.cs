using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour {
	[SerializeField]
	private GameObject GODuck;
	[SerializeField]
	private GameObject Audio;
	[SerializeField]
	private Text txtKill;
	[SerializeField]
	private Text txtLevel;
    [SerializeField]
    private Text txtLevelBoard;

    [SerializeField]
    private Text txtGameOver;

	[SerializeField]
	private Text txtScore;

    [SerializeField]
    private Text txtDucksToShoot;

	[SerializeField]
	private GameObject[] Bullets = new GameObject[5];
	public static int Bullet;

	[SerializeField]
	private GameObject GOGUI;

	[SerializeField]
	private GameObject strana1;
	[SerializeField]
	private GameObject strana2;

	[SerializeField]
	private GameObject Dog;

	public static int Kill;
	public static int LevelDuck;
	public static int GameLevel;
	public static int Wave;
	public static float WaveTime;

    [Header("ADS")]
    [SerializeField]
    AdMob Ads;
    public bool PlayAds;

	public static bool StartGame;
	public static int Duck;
	public static int Level;
	private GameObject LastDuck;

    // POSTAVLJA SE NA TRUE SAMO AKO ISTEKNE VREME U WAVE
    public static bool FinishWave = false;

	private bool Fired;

    public static int Score = 0;
	public static int KilledBird=0;

	public static bool WarClick= false;

	public static Collider2D[] ObjectDucks= new Collider2D[20];

    [Header("Lose and Win screens")]
    [SerializeField]
    private GameObject LoseScreen;
    [SerializeField]
    private GameObject WinScreen;

    [SerializeField]
    private GameObject Cam;
    [SerializeField]
    private GameObject optionsScreen;


    // Use this for initialization
    void Start () {
        if (Camera.main == null)
        {
            Cam.SetActive(true);
            GOGUI.SetActive(true);
            optionsScreen.SetActive(true);
        }
        Debug.Log(Time.timeScale);
        StartCoroutine(WaitForDog());
        txtDucksToShoot.text = "x " + ((((GameLevel + 1) % 4) + 1) * 3);
        StartGame = false;
		Duck = 0;
		Level = 1;
		Fired = false;
		Bullet = 5;
		Kill = LevelDuck = 0;
		GameLevel = 1;
		Wave = 0;
        txtLevel.text = "Level: " + GameLevel.ToString();
        txtLevelBoard.text = "Level " + GameLevel.ToString();
		txtScore.text = "Score: " + Score.ToString ();
		txtKill.text = "Kill: " + Kill.ToString ();
		strana1.transform.position = Camera.main.ViewportToWorldPoint (new Vector3(0,0.5f,0));
		strana2.transform.position = Camera.main.ViewportToWorldPoint (new Vector3(1,0.5f,0));
		strana1.transform.position = new Vector3(strana1.transform.position.x-0.7f,0, -1f);
		strana2.transform.position = new Vector3(strana2.transform.position.x+0.7f,0, -1f);
		KilledBird = 0;
        
	}

    IEnumerator WaitForDog()
    {
        while (Dog.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("findDuck"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        StartGame = true;
    }

    void StartDogAnimation(Animator animator, string name)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            return;

        animator.Play(name);
    }

	// Update is called once per frame
	void Update () {
        if (!StartGame)
        {
         //   Debug.Log("!StartGame");
            return;
        }

		if (StartGame && PlayPause.gamestart) {
			WaveTime -= Time.deltaTime;
			if (KilledBird>0) {
				if (KilledBird == 1) {
                    StartDogAnimation(Dog.GetComponent<Animator>(), "catchOne");
                    KilledBird -= 1;
				} else {
                    StartDogAnimation(Dog.GetComponent<Animator>(), "catchTwo");
                    KilledBird -= 2;
				}				
			}
					
		
			if (Duck == 0) {
                Debug.Log("Duck == 0");
				if (Wave < 3) {
                    FinishWave = false;
					Wave++;

                    Debug.Log(Wave);
					WaveTime = 7f;
                    for (int i = 1; i <= ((GameLevel + 1) % 4) + 1; i++)
                    {
                        Debug.Log("For petlja " + i);
                        SpawnDuck();
                    }
				}
                else {
					if ((Kill >= LevelDuck * 0.5)) {//Prelazi level
					//	GameLevel++;
                        WinGame();
					/*	Kill = 0;
						LevelDuck = 0;
						Wave = 0;
						txtLevel.text = "Level: " +GameLevel.ToString();*/
					} else {//GameOver
						GameOver();
					}
				}
				Bullet = 4;
				for(int i =0; i<5;i++)
					Bullets [i].GetComponent<Image> ().enabled = true;	
			
			}

            if (WaveTime <= 0)
            {
                FinishWave = true;
				KilledBird = 0;
            }

			if (FinishWave) {
                StartDogAnimation(Dog.GetComponent<Animator>(), "laughing");
			}		

			if (Input.touchCount <= 0 && WarClick)
				WarClick = false;
						
			if (Input.touchCount <= 0 && Fired) {
				Fired = false;
				//Audio.GetComponent<AudioSource> ().enabled = false;
			}
			if (Input.touchCount > 0 && Fired == false && Bullet >-1 && StartGame && !WarClick &&(Input.GetTouch(0).position.x<(9*Screen.width/10)|| Input.GetTouch(0).position.y < (5 * Screen.height / 6))) {
				
					if (Bullet < 5) {
						Fired = true;
						Audio.GetComponent<AudioSource> ().enabled = false;
						Audio.GetComponent<AudioSource> ().enabled = true;
						Bullets [Bullet].GetComponent<Image> ().enabled = false;
					}
					Bullet--;

			}
		
		//	Debug.Log (Bullet);
				

		
		}
		
	}

    private void WinGame()
    {
        int per = Kill / LevelDuck * 100;
        PlayerPrefs.SetInt("lock" + (GameLevel + 1).ToString(), 1);

        if(PlayerPrefs.GetInt("percentage" + GameLevel.ToString()) < per)
            {
            PlayerPrefs.SetInt("percentage" + GameLevel.ToString(), per);
            PlayerPrefs.SetInt("star1" + GameLevel.ToString(), 1);

            if (per >= 75)
                PlayerPrefs.SetInt("star2" + GameLevel.ToString(), 1);
            if (per >= 100)
                PlayerPrefs.SetInt("star3" + GameLevel.ToString(), 1);


        }

        WinScreen.SetActive(true);
        FinishWave = false;
        StartGame = false;
        Duck = 0;
        Level = 1;
        Fired = false;
        Bullet = 4;
        Kill = LevelDuck = 0;
        GameLevel ++;
        Wave = 0;
        txtGameOver.text = "Score: " + Score.ToString();
        Score = 0;
        KilledBird = 0;
    }

	private void GameOver()
	{
        LoseScreen.SetActive(true);
        FinishWave = false;
		StartGame = false;
		Duck = 0;
		Level = 1;
		Fired = false;
		Bullet = 4;
		Kill = LevelDuck = 0;
		Wave = 0;
        txtGameOver.text = "Score: "+ Score.ToString();
        Score = 0;
		KilledBird = 0;

	}

	public void SpawnDuck()
	{
        Debug.Log("SpawnDuck");
		ObjectDucks[Duck] = new Collider2D();
		LastDuck = Instantiate<GameObject> (GODuck);
		LastDuck.transform.position = new Vector2 (LastDuck.transform.position.x+Random.Range(-4,4),LastDuck.transform.position.y);
		LastDuck.GetComponent<CircleCollider2D> ().enabled = true;
		LastDuck.GetComponent<FireDuck> ().enabled = true;
        LastDuck.GetComponent<Rigidbody2D>().mass = 1f;

		if(Random.Range(0,100) % 2 == 0)
		LastDuck.tag="Red";
		else
		LastDuck.tag="Green";
		
		AddForceToDuck (LastDuck);
		ObjectDucks [Duck] = LastDuck.GetComponent<Collider2D>();
		Duck++;
		LevelDuck++;
		//Debug.Log (Duck);
		for (int i = 0; i < Duck; i++)
			Physics2D.IgnoreCollision (LastDuck.GetComponent<Collider2D>(),ObjectDucks[i]);
		txtKill.text = "Kill: " + Kill.ToString () + "/" + LevelDuck.ToString ();

	}

	public void AddForceToDuck(GameObject D)
	{
		int w = Random.Range (-100, 100);
		int h = Random.Range (30, 100);
		D.GetComponent<Rigidbody2D> ().AddForce(new Vector2(w/100f,h/100f)*200f);
	}

	public void ChangeStart(bool start){
        Debug.Log("ChangeStart");
		StartGame = start;
		if (StartGame) {
			WarClick = true;
			txtLevel.text = "Level: " +GameLevel.ToString();
			txtScore.text = "Score: " + Score.ToString ();
			txtKill.text = "Kill: " + Kill.ToString ();
		}
			
	}


	public void EnableComponent(GameObject Object)
	{
		Object.SetActive (true);
	}
	public void DisableComponent(GameObject Object)
	{
		Object.SetActive (false);
	}
}
