using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController {


    public int totalScore;
    public int totalMissed;
    public int money;
    private static GameController _current;
    public GameController() { }
    public static GameController Current 
    {
        get
        {
            if(_current == null)
            {
                _current = new GameController();
            }
            return _current;
        }
    }

	
    public void StartLevel()
    {
            Spawner.Current.StartSpawner();
    }

	void CalculateScore () {
        totalScore -= totalMissed;
	}

    void AddMissed(int missedPoints)
    {
        totalMissed += missedPoints;
    }

    void AddMoney(int amount)
    {
        money += amount;
    }

    bool SpendMoney(int amount)
    {
        bool success;
        if(amount > money)
        {
            success = false;
        }
        else
        {
            money -= amount;
            success = true;
        }
        return success;
    }

    void EndLevel()
    {

    }
}
