using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed = 10f;

    [Header("Отладка"), Space]
    public float GizmosOffset = 10f;
    public float DirectPointGizmoSize = 10f;
    public bool ShowSpeedGizmos = false;

    private Vector3 _directionalPoint = new Vector3();

    // Update is called once per frame
    void FixedUpdate()
    {
        // Получаем значения параметров перемещения
        var xAxe = Input.GetAxis("Horizontal");
        var yAxe = Input.GetAxis("Vertical");
        // Создаём точку на основе параметров осей
        var deltaPoint = new Vector3(xAxe, yAxe, 0);
        // Ограничиваем длину получившегося вектора 1f
        // Это необходимо, чтобы перемещение по диагонали не было быстрее чем по осям
        _directionalPoint = Vector3.ClampMagnitude(deltaPoint, 1f);
        // Добавляем коэффициент скорости и шаг дельта времени кадра
        var deltaOffset = _directionalPoint * (Speed * Time.fixedDeltaTime);
        // изменяем текущую позицию
        this.transform.position += deltaOffset;
    }

    /// <summary>
    /// Отрисока вспомогательных элеметов(гизмо)
    /// </summary>
    private void OnDrawGizmos()
    {
        if(ShowSpeedGizmos)
        {
            // Параметр увеличения элементов отрисоки
            var delta = Speed * GizmosOffset;

            var currentPosition = this.transform.position;

            // Рисуем сферу максимальной скорости перемещения
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(currentPosition, delta);

            // Рисуем вектор перемещения
            var direct = new Vector2(currentPosition.x + _directionalPoint.x * delta,
                currentPosition.y + _directionalPoint.y * delta);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(direct, DirectPointGizmoSize);
        }
    }
}
