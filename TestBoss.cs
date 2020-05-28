using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для определения макс и мин значения здоровья для атак. Значение настраиватся в диспетчере
/// </summary>
[System.Serializable]
public class HealthPercentBoss
{
    [Range(0, 100)]
    [SerializeField]int min, max;

    [HideInInspector]public int health { get; set; }

    public float GetMin()
    {
        return health* ((float)min / 100);
    }
    public float GetMax()
    {
        return health* ((float)max / 100);
    }
}

/// <summary>
/// Тестовый босс для демонстрации работы паттерна Мост
/// </summary>
public class TestBoss : MonoBehaviour
{
    SpecialAttackBoss special;
    public GameObject blast;

    public HealthPercentBoss percent;

    [SerializeField]private float timeLaunchAttack;

    private void Start()
    {
        special = null;
        percent.health = gameObject.GetComponent<Boss>().health;
    }

    private void Update()
    {
        
        if (special == null)
        {
            LaunchAttack(percent.GetMin(), percent.GetMax(), gameObject.GetComponent<Boss>().health);
        }
        else
        {
            if (!special.EndAttack(this.gameObject))
            {
                gameObject.GetComponent<Boss>().specialAttack = false;
                special = null;
            }
        }
    }
    /// <summary>
    /// Запуск атак, относительно количества здоровья
    /// </summary>
    /// <param name="min">Минимальный процент от здоровья</param>
    /// <param name="max">Максимальный процент от здоровья</param>
    /// <param name="health">Параметр здоровья</param>
    private void LaunchAttack(float min,float max,int health)
    {
        //Запуск первой специальной атаки
        if (health >= min && health <= max)
        {
            if (Time.time > timeLaunchAttack)
            {
                timeLaunchAttack = Time.time + Random.Range(5f, 15f);
                special = new SpecialAttackBoss(new FirstAttackBoss());
                gameObject.GetComponent<Boss>().specialAttack = true;
                special.Attack(this.gameObject);
                Debug.Log("First Special Attack Launch");
            }
        }
        //Запуск второй специальной атаки
        if (health < min)
        {
            if (Time.time > timeLaunchAttack)
            {
                timeLaunchAttack = Time.time + Random.Range(50f, 80f);
                special = new SpecialAttackBoss(new TwoAttack());
                gameObject.GetComponent<Boss>().specialAttack = true;
                special.Attack(this.gameObject, blast);
                Debug.Log("Two Special Attack Launch");
            }
        }
      
    }
}
