using TMPro;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerBuildEvents TowerBuildEvents;
    [SerializeField] private TowerUpgradeEvents TowerUpgradeEvents;
    [SerializeField] private EnemyKillEvents EnemyKillEvents;

    [Header("Gold Controller Parametres")]
    [SerializeField] private int startGold;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI TMP_Gold;
    [SerializeField] private string afterGoldSymbol;

    private int gold;

    public static GoldController Instance { get { return instance; } }
    private static GoldController instance;

    public delegate void MethodContainerWithInt(int NewGoldValue);
    public event MethodContainerWithInt OnGoldValueChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SubscribeAtEvents();

        SetupStartGold();
    }

    private void SubscribeAtEvents()
    {
        OnGoldValueChanged += NewGoldValue;
        TowerBuildEvents.OnTowerBuild += TakeGoldForBuild;
        EnemyKillEvents.OnEnemyDie += AddGoldForEnemy;
    }

    private void TakeGoldForBuild(TowerData towerData)
    {
        gold -= towerData.BuildCost;
        OnGoldValueChanged(gold);
    }

    private void AddGoldForEnemy(EnemyData enemyData)
    {
        gold += enemyData.EnemyStats.GoldForKill;
        OnGoldValueChanged(gold);
    }
    
    private void NewGoldValue(int value)
    {
        TMP_Gold.text = $"{gold}{afterGoldSymbol}";
    }

    public void SetupStartGold()
    {
        gold = startGold;
        OnGoldValueChanged(gold);
    }

    public void TakeGoldForUpgrade(int upgradeCost)
    {
        gold -= upgradeCost;
        OnGoldValueChanged(gold);
    }

    public void AddGoldForTowerSell(int sellCost)
    {
        gold += sellCost;
        OnGoldValueChanged(gold);
    }

    public int Gold
    {
        get
        {
            return gold;
        }
    }
}