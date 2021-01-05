using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Components")]
    [SerializeField] private Transform meshParent;
    [SerializeField] private GameObject defaultMesh;

    private TowerData myTowerData;
    private Foundation myFoundation;

    private GameObject towerMeshGO;
    private Transform shootingPosition;

    public void SetupTower(TowerData towerData, Foundation foundation)
    {
        myTowerData = towerData;
        myFoundation = foundation;

        defaultMesh.SetActive(false);

        towerMeshGO = Instantiate(myTowerData.LevelsArray[0].MeshPrefab, meshParent);
        TowerMeshPrefab towerMesh = towerMeshGO.GetComponent<TowerMeshPrefab>();
        shootingPosition = towerMesh.ShootingPosition;

        myFoundation.Deactivate();
    }
}