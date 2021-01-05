using UnityEngine;

public class TowerFoundation : MonoBehaviour
{
    [Header("Foundation")]
    [SerializeField] private bool isNeedToSetupFoundationOnAwake;
    [SerializeField] private Foundation foundation;

    private void Start()
    {
        if(isNeedToSetupFoundationOnAwake == true)
            SetupFoundationComponents();
    }

    private void OnMouseUp()
    {
        TowerBuildEvents.InvokeOnTowerFoundationClickEvent(this.foundation);
    }

    public void SetupFoundationComponents()
    {
        foundation.transform = transform;
        foundation.towerParent = gameObject.transform.Find("TowerParent");
        foundation.position = transform.position;

        foundation.boxCollider = GetComponent<BoxCollider>();
        Transform model = transform.GetChild(0);
        foundation.meshGO = (model.GetChild(0) == true) ? model.GetChild(0).gameObject : model.gameObject;
    }
}

[System.Serializable]
public class Foundation
{
    public Transform transform;
    public Transform towerParent;
    public Vector3 position;

    public BoxCollider boxCollider;
    public GameObject meshGO;

    public void Deactivate()
    {
        boxCollider.enabled = false;
    }

    public void Activate()
    {
        boxCollider.enabled = true;
    }
}