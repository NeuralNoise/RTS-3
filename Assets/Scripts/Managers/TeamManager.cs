using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamManager : MonoBehaviour {
    public static TeamManager instance = null;

    private Dictionary<Team, List<Transform>> mTeams = new Dictionary<Team, List<Transform>>();
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	public void AddToTeam(Team team, Transform trans)
    {
        if(mTeams.ContainsKey(team))
        {
            List<Transform> transList = mTeams[team];
            if (transList.Contains(trans) == false)
                transList.Add(trans);
        }
    }

    public void RemoveFromTeam(Team team, Transform trans)
    {
        if (mTeams.ContainsKey(team))
        {
            List<Transform> transList = mTeams[team];
            if (transList.Contains(trans))
                transList.Remove(trans);
        }
    }

    public List<Transform> GetTeamMembers(Team team)
    {
        if (mTeams.ContainsKey(team))
            return mTeams[team];

        return null;
    }
}
