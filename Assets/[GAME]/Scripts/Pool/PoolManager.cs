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
    public GameObject bulletDecal;
    public GameObject moneyImage;
    public GameObject stickmanVisual;
    public GameObject targetRemoved;

    [Header("Pools")]
    [HideInInspector] public PoolingPattern poolBullet;
    [HideInInspector] public PoolingPattern poolBulletDecal;
    [HideInInspector] public PoolingPattern poolMoneyImage;
    [HideInInspector] public PoolingPattern poolStickmanVisual;
    [HideInInspector] public PoolingPattern poolTargetRemoved;

    void Init()
    {
        poolBullet = new PoolingPattern(bullet);
        poolBullet.FillPool(10);

        poolBulletDecal = new PoolingPattern(bulletDecal);
        poolBulletDecal.FillPool(10);

        poolMoneyImage = new PoolingPattern(moneyImage);
        poolMoneyImage.FillPool(10);

        poolStickmanVisual = new PoolingPattern(stickmanVisual);
        poolStickmanVisual.FillPool(20);

        poolTargetRemoved = new PoolingPattern(targetRemoved);
        poolTargetRemoved.FillPool(5);
    }

    public Transform GenerateVisualStickman()
    {
        return poolStickmanVisual.PullObjFromPool().transform;
    }
}
