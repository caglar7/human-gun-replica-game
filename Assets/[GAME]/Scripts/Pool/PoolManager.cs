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

    [Header("Pools")]
    [HideInInspector] public PoolingPattern poolBullet;
    [HideInInspector] public PoolingPattern poolMoneyImage;

    void Init()
    {
        poolBullet = new PoolingPattern(bullet);
        poolBullet.FillPool(10);

        poolMoneyImage = new PoolingPattern(moneyImage);
        poolMoneyImage.FillPool(10);
    }
}
