using System;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private GameObject _menuPrefab;
    [SerializeField] private GameObject _levelPrefab;

    private void Awake()
    {
        Instantiate(_menuPrefab);
    }
}