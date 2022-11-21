using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = System.Random;

public class Entity : MonoBehaviour
{
    [AllowNull] [SerializeField] public Slider healthBar;

    [SerializeField] public bool isPlayer;
    [AllowNull] public Weapon weapon;
    [AllowNull] public NavMeshAgent agent;

    private Vector3 _critPoint;
    private bool _isagentNotNull;
    private bool _ishealthBarNotNull;
    private bool _isHealthBarNotNull;
    private Camera _mainCamera;

    private Rigidbody _rb;
    private StatsManager _statsManager;

    private void Start()
    {
        _mainCamera = Camera.allCameras[0];
        _isagentNotNull = agent != null;
        _ishealthBarNotNull = healthBar != null;
        _isHealthBarNotNull = healthBar != null;
        _rb = GetComponent<Rigidbody>();
        _statsManager = GetComponent<StatsManager>();
    }

    private void Update()
    {
        if (_ishealthBarNotNull)
            healthBar.transform.LookAt(healthBar.transform.position + _mainCamera.transform.rotation * Vector3.back,
                _mainCamera.transform.rotation * Vector3.up);


        if (_isagentNotNull)
        {
            if (Vector3.Distance(transform.position, _mainCamera.transform.position) > 3.5)
                agent.SetDestination(_mainCamera.transform.position);
            else if (Vector3.Distance(transform.position, _mainCamera.transform.position) < 3) agent.ResetPath();
        }
    }


    // public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
// cooles c# feature
    public bool damage(float damage)
    {
        var dead = _statsManager.damage(damage);
        if (dead)
        {
            var r = new Random();
            // Calculate crit point
            _rb.AddForce(new Vector3(r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10)), ForceMode.Impulse);
            //TODO: Add particle effect
            if (isPlayer)
            {
                //TODO: Respawn player or so ( show press ALT + F + 4 to respawn)
            }
        }

        UpdateHealthBar();
        return dead;
    }

    public bool damageEntity(Entity entity, float damage, Vector3 hitInfoPoint)
    {
        damage *= _statsManager.damageMultiplier;
        if (isCrit(hitInfoPoint)) damage *= _statsManager.criticalDamageMultiplier;
        return entity.damage(damage);
    }

    public void heal(float heal)
    {
        _statsManager.heal(heal);
        UpdateHealthBar();
    }

    public bool isCrit(Vector3 point)
    {
        if (.2 > Vector3.Distance(_critPoint, getLocal(point))) return true;
        return false;
    }

    public Vector3 getLocal(Vector3 point)
    {
        return transform.position - point;
    }

    private void UpdateHealthBar()
    {
        if (_isHealthBarNotNull)
            healthBar.value = _statsManager.currentHealth / _statsManager.maxHealth;
    }
}