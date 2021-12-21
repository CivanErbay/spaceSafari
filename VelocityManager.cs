using UnityEngine;
using UnityEngine.UI;

public class VelocityManager : MonoBehaviour
{

    private Text VelocityUI;
    private int currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        VelocityUI = gameObject.GetComponent<Text>();
        VelocityUI.text = "Velocity: " + currentVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        VelocityUI.text = "Velocity: " + currentVelocity;
        currentVelocity = PlayerPrefs.GetInt("PlayerVelocity");
    }
}
