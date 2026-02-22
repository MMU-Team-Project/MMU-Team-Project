using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    [SerializeField]
    private float timeUntilLvlUp;

    [SerializeField]
    private float levelUpTime;

    public int difficultyLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeUntilLvlUp = levelUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilLvlUp -= Time.deltaTime;

        if (timeUntilLvlUp <= 0)
        {
            difficultyLevel += 1;
            timeUntilLvlUp = levelUpTime;
        }
    }
}
