using UnityEngine;
using UnityEngine.UI;
public class FireDuck : MonoBehaviour {

	[SerializeField]
	private GameObject Audio;
	[SerializeField]
	private Text txtKill;
    [SerializeField]
    private Sprite BD;
	[SerializeField]
	private Sprite BD1;
	[SerializeField]
	private Text txtScore;
    [SerializeField]
    private Text debugText;

	public bool PFired;
	// Use this for initialization
	void Start () {
        Animator animator = GetComponent<Animator>();
		PFired = false;
        if (tag == "Green")
        {
            animator.Play("greenFly");
        }
        else
        {
            animator.Play("redFly");
        }

        animator.SetFloat("speedMultiplier", 1 + GamePlay.GameLevel * 0.1f);
        Debug.Log(animator.GetFloat("speedMultiplier"));
	}
	
	// Update is called once per frame
	void Update () {
        if (!GamePlay.StartGame || GamePlay.Bullet <= -1 || !PlayPause.gamestart)
            return;

		if (Input.touchCount > 0) {
            debugText.text = "Passed Input.touchCount";
			if (!PFired && !GamePlay.WarClick) {
                debugText.text = "Passed !PFired && !GamePlay.WarClick";
				//Audio.GetComponent<AudioSource> ().enabled = true;
				var point = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			
				float x1 = gameObject.transform.position.x;
				float y1 = gameObject.transform.position.y;
				if (Mathf.Sqrt ((point.x - x1) * (point.x - x1) + (point.y - y1) * (point.y - y1)) <= 0.7f) {
					if (gameObject.GetComponent<Rigidbody2D> ().mass == 1f) {
						//Destroy (gameObject);
						//GamePlay.Duck--;
						gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;//
						gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1.5f;
						gameObject.GetComponent<Rigidbody2D> ().mass = 1.5f;
						GamePlay.Kill++;
						txtKill.text = "Kill: " + GamePlay.Kill.ToString () + "/" + GamePlay.LevelDuck.ToString ();
                        ////////////////////////////
                        // POKRETANJE ANIMACIJE ZA SMRT PATKE -Lukaz
                        if (gameObject.tag == "Green")
                        {
                            Kill(gameObject.GetComponent<Animator>(), "greenDie");
                            debugText.text = "<color=\"green\">greenDuckDied\"</color>";
                        }
                        else
                        {
                            Kill(gameObject.GetComponent<Animator>(), "redDie");
                            debugText.text = "<color=\"red\">redDuckDied\"</color>";
                        }
                        ////////////////////////////
                        GamePlay.Score++;

						txtScore.text = "Score: " + GamePlay.Score.ToString ();
                    }
				}
				PFired = true;
			}

		} else if (PFired)
			PFired = false;
	}

    // Metoda za ubijanje patke -Lukaz
    public void Kill(Animator animator, string deathAnimation)
    {
        animator.Play(deathAnimation);
    }

}
