using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CellArray _cm;
    void Start()
    {
        Field field = new Field(10, 10, 20);
        String res = "";

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                res += field.GetValue(i, j) + " ";
            }

            res += "\n";
        }

        Debug.Log(res);

        //cm.Init(10, 10, transform.position);
    }
}
