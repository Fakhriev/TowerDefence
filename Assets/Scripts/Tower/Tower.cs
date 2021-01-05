using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerUpgrade TowerUpgrade;

    [Header("Tower Components")]
    [SerializeField] private TowerComponents towerComponents;

    private TowerData myTowerData;
    private Foundation myFoundation;

    public void SetupTower(TowerData towerData, Foundation foundation)
    {
        myTowerData = towerData;
        myFoundation = foundation;

        towerComponents.DefaultMesh.SetActive(false);
        TowerUpgrade.SetupTowerLevelOne();

        myFoundation.Deactivate();
    }

    public void Upgrade()
    {
        TowerUpgrade.Upgrade();
    }

    public TowerComponents TowerComponenets
    {
        get
        {
            return towerComponents;
        }
    }

    public TowerData MyTowerData
    {
        get
        {
            return myTowerData;
        }
    }
}

[System.Serializable]
public class TowerComponents
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Transform meshParent;
    [SerializeField] private GameObject defaultMesh;

    public BoxCollider BoxCollider
    {
        get
        {
            return boxCollider;
        }
    }

    public Transform MeshParent
    {
        get
        {
            return meshParent;
        }
    }

    public GameObject DefaultMesh
    {
        get
        {
            return defaultMesh;
        }
    }
}