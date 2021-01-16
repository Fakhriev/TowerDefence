using UnityEngine;

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