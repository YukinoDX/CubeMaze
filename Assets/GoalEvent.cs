using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEvent : MonoBehaviour
{
    [SerializeField] private float delay;
    private GameObject mazeCreater;

    // Start is called before the first frame update
    void Start()
    {
        mazeCreater = GameObject.Find("MazeCreater");
        Debug.Assert(mazeCreater != null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HiddenWall());
    }

    IEnumerator HiddenWall()
    {
        int size = CreateMaze.size;
        GameObject[,,] mazeObject = mazeCreater.GetComponent<CreateMaze>().mazeObject;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    if (mazeObject[i, j, k].name == "path(Clone)")
                    {
                        continue;
                    }

                    mazeObject[i, j, k].GetComponent<Renderer>().enabled = false;
                    yield return new WaitForSeconds(delay);
                }
            }
        }
    }
}
