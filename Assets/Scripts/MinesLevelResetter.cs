using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesLevelResetter : MonoBehaviour
{
    [SerializeField] public GameObject[] stalactiteList;
    //[SerializeField] public float[] xCoords;
    //[SerializeField] public float[] yCoords;

    public void resetScreens()
    {
        int counter = 0;

        foreach (GameObject stalactite in stalactiteList)
        {
            stalactite.transform.GetChild(0).GetComponent<StalactiteFall>().resetPosition();
            //stalactite.transform.position = new Vector2(xCoords[counter], yCoords[counter]);
        }
    }
}
