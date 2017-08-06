using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextGenerator : MonoBehaviour {

    public string[] texts;

	// Use this for initialization
	public void GenerateText () {
        if (texts.Length > 0)
            GetComponent<Text>().text = texts[Random.Range(0, texts.Length - 1)];
        else
        {
            Debug.Log("There are no custom texts in the quit dialog box." +
                "You can insert a text in the 'TextGenerator' script.");
            GetComponent<Text>().text = "Are you sure you want to quit?";
        }
	}
}
