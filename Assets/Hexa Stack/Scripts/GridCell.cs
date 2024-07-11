using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GridCell : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Hexagon hexagonPrefab;

    [Header("Settings")]
    [OnValueChanged("GenerateInitialHexagons")]
    [SerializeField] private Color[] hexagonColors;

    public HexStack Stack { get; private set; }

    public bool IsOccupied 
    {
        get => Stack != null;
        private set { }
    }

    private void Start()
    {
        if(hexagonColors.Length > 0)
            GenerateInitialHexagons();
    }

    public void AssignStack(HexStack stack)
    {
        Stack = stack;
    }


    private void GenerateInitialHexagons()
    {
        while (transform.childCount > 1)
        {
            Transform t = transform.GetChild(1);
            t.SetParent(null);
            DestroyImmediate(t.gameObject);
        }
        
        Stack = new GameObject("Initial Stack").AddComponent<HexStack>();
        Stack.transform.SetParent(transform);
        Stack.transform.localPosition = Vector3.up * .2f;

        for (int i = 0; i < hexagonColors.Length; i++)
        {
            Vector3 spawnPosition = Stack.transform.TransformPoint(Vector3.up * i * .2f);
            
            Hexagon hexagonInstance = Instantiate(hexagonPrefab, spawnPosition, Quaternion.identity);
            hexagonInstance.Color = hexagonColors[i];

            Stack.Add(hexagonInstance);
        }
    }
}
