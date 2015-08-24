using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Win : MonoBehaviour {

    public string winLevel;
    public int targetScore = 500;
    static float plusRatio = 10;
    static string[] grades = {"A", "A-", "B+", "B", "B-", 
                                 "C+", "C", "C-", "D+", "D", 
                                 "D-", "E+", "E", "E-"};
    public Text gradeText;
    TotalScore score;
    bool submitted = false;

	// Use this for initialization
	void Start () {
        score = FindObjectOfType<TotalScore>();
	}

    //OnGUI is called even if time is frozen
    void OnGUI()
    {
        if (gameObject.activeSelf)
        {
            string g = "A";
            if(score.score < targetScore) {
                g = grades[Mathf.FloorToInt(grades.Length - grades.Length * score.score / targetScore)];
            }
            else if (score.score >= targetScore + plusRatio)
            {
                int plusses = Mathf.FloorToInt(((score.score - targetScore) / targetScore) * plusRatio);
                for (int i = 0; i < plusses; i++)
                {
                    g += "+";
                }
            }
            gradeText.text = "GRADE: " + g;
            Time.timeScale = 0;
            if (Input.GetAxisRaw("Submit") != 0 && !submitted)
            {
                submitted = true;
                MusicPlayer.Instance.finalScore += score.score;
                MusicPlayer.Instance.letterGrades.Add(g);
                Time.timeScale = 1;
                Application.LoadLevel(winLevel);
            }
        }
    }
}
