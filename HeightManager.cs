using UnityEngine;
using UnityEngine.UI;

public class HeightManager : MonoBehaviour
{
    public Text HeightUI;
    private int currentHeight;

    private void Start() {
        HeightUI = gameObject.GetComponent<Text>();
        HeightUI.text="Height: " + currentHeight;
    }

    private void Update() {
        HeightUI.text="Height: " + currentHeight;
        currentHeight = PlayerPrefs.GetInt("PlayerHeight");
    }
}
