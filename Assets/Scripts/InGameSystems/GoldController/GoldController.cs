using TMPro;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerBuildEvents TowerBuildEvents;
    [SerializeField] private TowerUpgradeEvents TowerUpgradeEvents;
    [SerializeField] private EnemyKillEvents EnemyKillEvents;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI TMP_Gold;
    [SerializeField] private string afterGoldSymbol;

    private int startGold;
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
    private void SetupStartGold()
    {
        gold = startGold;
        OnGoldValueChanged(gold);
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

    public void SetStartGoldValue(int startGold)
    {
        this.startGold = startGold;
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