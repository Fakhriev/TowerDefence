using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Rounds Config", menuName = "Game Rounds Config SO", order = 51)]
public class GameRoundsConfig_SO : ScriptableObject
{
    [SerializeField] private Round[] rounds = new Round[0];

    public Round[] Rounds
    {
        get
        {
            return rounds;
        }
    }

    public void AddNewRound()
    {
        if(rounds.Length == 0)
        {
            //Debug.Log("Create First Round");
            return;
        }

        List<Round> roundsList = rounds.ToList();
        Round round = roundsList.Last();

        roundsList.Add(round);
        rounds = roundsList.ToArray();
    }
}