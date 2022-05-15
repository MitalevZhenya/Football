using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedObstacle : BaseObstacles
{
    [SerializeField] private GameObject destroyedVersionPrefab;
    [field:SerializeField] public GameObject DestroyedVersion { get; private set; }

    private void Awake()
    {
        InstantiateDestroyVersion();
    }
    public void InstantiateDestroyVersion()
    {
        if (DestroyedVersion != null) Destroy(DestroyedVersion);

        DestroyedVersion = Instantiate(destroyedVersionPrefab);
        DestroyedVersion.transform.position = transform.position;
        DestroyedVersion.transform.rotation = transform.rotation;

        DestroyedVersion.SetActive(false);
    }
}