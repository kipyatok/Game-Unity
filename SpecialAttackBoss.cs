using UnityEngine;


/// <summary>
/// Класс "мост" для создания и вызова специальных атак у босса
/// </summary>
public class SpecialAttackBoss 
{
    protected SpecialAttack _specialAttack;
    public SpecialAttackBoss(SpecialAttack specialAttack)
    {
        this._specialAttack = specialAttack;
    }

    /// <summary>
    /// Первая специальная атака босса. Где происходит ускоренное падание в низ
    /// </summary>
    /// <param name="boss">Игровой объект босса</param>
    public virtual void Attack(GameObject boss)
    {
        _specialAttack.AttackBoss(boss);
    }
    /// <summary>
    /// Вторая специальная атака босса.
    /// Босс спускается в центр выпускает 2 луча, лучи начинают крутится в одну из сторон с наростанием скорости.
    /// boss - игровой объект босса
    /// blast - игровой объект луча
    /// </summary>
    /// <param name="boss">Игровой объект босса</param>
    /// <param name="blast">Игровой объект луча</param>
    public virtual void Attack(GameObject boss,GameObject blast)
    {
        _specialAttack.AttackBoss(boss,blast);
    }
    /// <summary>
    /// Определяет закончилась атака или нет. True значит да, False значит нет
    /// </summary>
    /// <returns></returns>
    public virtual bool EndAttack(GameObject boss)
    {
        return _specialAttack.GetEndAttack(boss);
    }
}

/// <summary>
/// Абстрактный класс для связи с "мостом" классом SpecialAttackBoss
/// </summary>
public abstract class SpecialAttack : MonoBehaviour
{
    public virtual void AttackBoss(GameObject boss) { }
    public virtual void AttackBoss(GameObject boss, GameObject blast) { }
    protected bool EndAttack { get;  set; }
    public virtual bool GetEndAttack(GameObject boss) { return EndAttack; }
}

/// <summary>
/// Первая спец атака. Резкое падение объекта вниз и медленное поднимание
/// </summary>
class BasicAttack : SpecialAttack
{
    /// <summary>
    /// Запуск второй специальной атаки у объекта. С подключением компанента FirstAttackBosss.
    /// Если компанент добавлен, то просто его активируем.
    /// boss -  объект к которому подключаем
    /// </summary>
    /// <param name="boss">Объект босса</param>
    public override void AttackBoss(GameObject boss)
    {
        if(!boss.GetComponent<BasicAttackBoss>())
        {
            boss.AddComponent<BasicAttackBoss>();
        }
        else
        {
            boss.GetComponent<BasicAttackBoss>().enabled = true;
        }
    }

    /// <summary>
    /// Проверяем активен ли компанент атаки у объекта. Если нет значит атака завершена или не начата.
    /// Возращает False если не активен. True если активен
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public override bool GetEndAttack(GameObject boss)
    {
        if (boss.GetComponent<BasicAttackBoss>())
        {
            if (boss.GetComponent<BasicAttackBoss>().enabled)
            {
                return true;
            }
        }
        return false;
    }
}

/// <summary>
/// Вторая специальная атака. Объект движется в центр. Запускает лазеры и начинает их крутить.
/// </summary>
class TwoAttack : SpecialAttack
{

    /// <summary>
    /// Запуск второй специальной атаки у объекта. С подключением компанента TwoAttackBosss.
    /// Если компанент добавлен, то просто его активируем.
    /// boss -  объект к которому подключаем
    /// blast - объект лазера
    /// </summary>
    /// <param name="boss">Объект босса</param>
    /// <param name="blast">Объект лазера</param>
    public override void AttackBoss(GameObject boss, GameObject blast)
    {
        if (!boss.GetComponent<BlastAttackBoss>()) // Если компанент не добавлен, то добавляем его и присваиваем лазер
        {
            boss.AddComponent<BlastAttackBoss>();
            TwoAttackBosss _boss = boss.GetComponent<BlastAttackBoss>();
            _boss.blactBullet = blast;
        }
        else // Иначе просто запускаем
        {
            boss.GetComponent<BlastAttackBoss>().enabled = true;
        }
    }
    /// <summary>
    /// Проверяем активен ли компанент атаки у объекта. Если нет значит атака завершена или не начата.
    /// Возращает False если не активен. True если активен
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public override bool GetEndAttack(GameObject boss)
    {
        if (boss.GetComponent<BlastAttackBoss>())
        {
            if (boss.GetComponent<BlastAttackBoss>().enabled)
            {
                return true;
            }
        }
        return false;
    }
}



