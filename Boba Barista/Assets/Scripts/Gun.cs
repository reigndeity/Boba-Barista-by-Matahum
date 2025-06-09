using UnityEngine;

public class Gun : MonoBehaviour
{
    private GunRotation m_gunRotation;
    private GunVisuals m_gunVisuals;
    private IngredientButtons m_ingredientButtons;
    private ProjectileSequence m_projectileSequence;
    private GunShooting m_gunShooting;
    private GunAnimator m_gunAnimator;
    private AmmoAnimator m_ammoAnimator;

    [Header("Gun Variables")]
    public bool canReload = true;

    void Start()
    {
        m_gunRotation = GetComponentInChildren<GunRotation>();
        m_gunVisuals = GetComponentInChildren<GunVisuals>();
        m_ingredientButtons = GetComponentInChildren<IngredientButtons>();
        m_projectileSequence = GetComponentInChildren<ProjectileSequence>();
        m_gunShooting = GetComponentInChildren<GunShooting>();
        m_gunAnimator = GetComponentInChildren<GunAnimator>();
        m_ammoAnimator = GetComponentInChildren<AmmoAnimator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_ingredientButtons.QButtonPressed();
            m_projectileSequence.AddToSequence("Q");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_ingredientButtons.WButtonPressed();
            m_projectileSequence.AddToSequence("W");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_ingredientButtons.EButtonPressed();
            m_projectileSequence.AddToSequence("E");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_ingredientButtons.RButtonPressed();
            m_projectileSequence.AddToSequence("R");
        }

        if (Input.GetMouseButtonDown(0) && m_projectileSequence.BubbleSequence != "")
        {
            m_gunAnimator.ShootAnimation();
            m_ammoAnimator.AmmoShoot();
        }

        if (Input.GetMouseButtonDown(1) && m_projectileSequence.BubbleSequence != "" && canReload)
        {
            m_ammoAnimator.ReloadAnimation();
            canReload = false;
        }
    }
}
