using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子柜台视觉管理
/// </summary>
public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform plateVisual_Prefab;

    private List<GameObject> plates = new List<GameObject>(); 
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plates[plates.Count - 1];
        plates .Remove(plateGameObject);
        Destroy (plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual_Prefab, spawnPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform .localPosition = new Vector3(0, plateOffsetY * plates.Count, 0);
        plates.Add(plateVisualTransform.gameObject);
    }

}
