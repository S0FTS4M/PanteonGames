using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class ProducibleController : MonoBehaviour
{

    public static ProducibleController Instance;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public GameObject ProduceUnit(UnitBase producibleUnitBase, UnitBase producerUnitBase)
    {


        return null;
    }
}
