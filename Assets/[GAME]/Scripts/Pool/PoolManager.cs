using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton

    public static PoolManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        Init();
    }

    #endregion

    [Header("Objects For Pooling")]
    public GameObject bullet;
    public GameObject moneyImage;
    public GameObject stickmanVisual;

    [Header("Pools")]
    [HideInInspector] public PoolingPattern poolBullet;
    [HideInInspector] public PoolingPattern poolMoneyImage;
    [HideInInspector] public PoolingPattern poolStickmanVisual;

    void Init()
    {
        poolBullet = new PoolingPattern(bullet);
        poolBullet.FillPool(10);

        poolMoneyImage = new PoolingPattern(moneyImage);
        poolMoneyImage.FillPool(10);

        poolStickmanVisual = new PoolingPattern(stickmanVisual);
        poolStickmanVisual.FillPool(20);
    }

    public Transform GenerateVisualStickman()
    {
        return poolStickmanVisual.PullObjFromPool().transform;
    }
}
