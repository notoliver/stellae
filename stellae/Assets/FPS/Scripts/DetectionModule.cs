using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DetectionModule : MonoBehaviour
{
    [Tooltip("The point representing the source of target-detection raycasts for the enemy AI")]
    public Transform detectionSourcePoint;
    [Tooltip("The max distance at which the enemy can see targets")]
    public float detectionRange = 20f;
    [Tooltip("The max distance at which the enemy can attack its target")]
    public float attackRange = 10f;
    [Tooltip("Time before an enemy abandons a known target that it can't see anymore")]
    public float knownTargetTimeout = 4f;
    [Tooltip("Optional animator for OnShoot animations")]
    public Animator animator;

    public UnityAction onDetectedTarget;
    public UnityAction onLostTarget;

    public GameObject knownDetectedTarget { get; private set; }
    public bool isTargetInAttackRange { get; private set; }
    public bool isSeeingTarget { get; private set; }
    public bool hadKnownTarget { get; private set; }

    protected float m_TimeLastSeenTarget = Mathf.NegativeInfinity;

    ActorsManager m_ActorsManager;

    const string k_AnimAttackParameter = "Attack";
    const string k_AnimOnDamagedParameter = "OnDamaged";

    protected virtual void Start()
    {
        m_ActorsManager = FindObjectOfType<ActorsManager>();
        DebugUtility.HandleErrorIfNullFindObject<ActorsManager, DetectionModule>(m_ActorsManager, this);
    }

    public virtual void HandleTargetDetection(Actor actor, Collider[] selfColliders)
    {
        
    }

    public virtual void OnLostTarget()
    {

    }
    public virtual void OnDetect()
    {

    }

    public virtual void OnDamaged(GameObject damageSource)
    {

    }

    public virtual void OnAttack()
    {

    }
}
