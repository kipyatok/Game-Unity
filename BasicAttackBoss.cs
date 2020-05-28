using UnityEngine;
/// <summary>
/// Первая спец атака. Резкое падение объекта вниз и медленное поднимание
/// </summary>
public class BasicAttackBosss : MonoBehaviour
{
    float speedDown = 20f; // Скорость падания
    float speedUP = 5f; // Скорость подымания
    bool mflag = true; // Флаг для понимаю опустился объект или нет
    private Vector3 start; // Начальные координаты
    private Vector3 end; // Координаты точки падения

    private void Update()
    {
        if (mflag)
        { 
            //Падаем вниз
            transform.position = Vector3.MoveTowards(transform.position, end, speedDown * Time.deltaTime);
            if (Vector3.Distance(transform.position, end) < 0.01f)
            {
                mflag = !mflag;
            }
        }
        else
        {
            //Подымаемся вверх
            transform.position = Vector3.MoveTowards(transform.position, start, speedUP * Time.deltaTime);
            //Если достигли вверха, отключаем компанент
            if (Vector3.Distance(transform.position, start) < 0.01f)
            {
                gameObject.GetComponent<FirstAttackBosss>().enabled = false;
                mflag = !mflag;
            }
        }
    }

    private void Start()
    {
        //Формируем координаты начала и конца
        start = transform.position;
        float y = Camera.main.ViewportToWorldPoint(Vector2.zero).y + 3f;
        end = new Vector3(transform.position.x, y, transform.position.z);
    }
}

