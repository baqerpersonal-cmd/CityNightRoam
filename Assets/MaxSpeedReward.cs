using UnityEngine;

public class MaxSpeedReward : MonoBehaviour
{
    private Rigidbody rb;
    private float currentStreak = 0f;
    private float highScore = 0f;
    private float speedKmh = 0f;
    private const float THRESHOLD = 120f; // km/h

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) rb = GetComponentInParent<Rigidbody>();
        if (!rb) Debug.LogError("No Rigidbody found on car!");
        else Debug.Log("Rigidbody found!");

        highScore = PlayerPrefs.GetFloat("BestStreak", 0f);
    }

    void Update()
    {
        if (rb == null) return;

        speedKmh = rb.linearVelocity.magnitude * 3.6f;

        if (speedKmh >= THRESHOLD)
        {
            currentStreak += Time.deltaTime;

            if (currentStreak > highScore)
            {
                highScore = currentStreak;
                PlayerPrefs.SetFloat("BestStreak", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            currentStreak = 0f;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 42;
        style.fontStyle = FontStyle.Bold;

        
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(20, 20, 500, 60),
            "Speed: " + speedKmh.ToString("F0") + " km/h", style);

        
        style.normal.textColor = speedKmh >= THRESHOLD ? Color.green : Color.yellow;
        GUI.Label(new Rect(20, 80, 500, 60),
            "Streak: " + currentStreak.ToString("F1") + "s", style);

        
        style.normal.textColor = Color.cyan;
        GUI.Label(new Rect(Screen.width - 320, 20, 300, 60),
            "Best: " + highScore.ToString("F1") + "s", style);
    }

}
