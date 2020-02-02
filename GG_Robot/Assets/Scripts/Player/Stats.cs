using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static Stats _instance;

    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currHealth;

    [SerializeField]
    private GameEvent _playerDeath;

    public static Stats Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }

    private void Start()
    {
        _currHealth = _maxHealth;
    }

    private void Update()
    {
        
    }

    public void MaxHeal()
    {
        _currHealth = _maxHealth;
    }

    public void Heal(int heal)
    {
        _currHealth = Mathf.Clamp(_currHealth + heal, 0, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _currHealth -= damage;

        if(_currHealth <= 0)
        {
            AudioManager.Instance.Play("Death");
            _playerDeath.Raise();
        }
    }

    public void KillPlayer()
    {
        _currHealth = 0;

        AudioManager.Instance.Play("Death");
        _playerDeath.Raise();
    }
}
