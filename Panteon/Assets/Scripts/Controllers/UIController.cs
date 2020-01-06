using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    Transform contentTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearProductsMenu()
    {
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            SimplePool.Despawn(contentTransform.GetChild(i).gameObject);
        }
    }
    public void CreateBuildingsProudctsPanel()
    {
        foreach (string buildingTypeName in Enum.GetNames(typeof(BuildingType)))
        {
            //for every type of building we need to create a UI object from pool

            //assign names colors and click events to the UI elements
        }
        //Buildings 
        // SimplePool.Despawn()
    }
}
