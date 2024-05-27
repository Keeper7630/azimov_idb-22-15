using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    public GameObject tailstock;    // Задняя бабка
    public GameObject handle;       // Рукоятка задней бабки
    public Text field;  // Поле с координатами задней бабки и резцедерки 

    public GameObject tool_hold;            // Резцедержка
    public GameObject handle_transverse;    // Поперечная ручка
    public GameObject handle_lateral;       // Продольная ручка
    public GameObject cartrige;

    // Стартовая позиция задней бабки и рукоятки задней бабки
    public double start_position_tailstock_x, start_position_tailstock_y, start_position_tailstock_z;
    public double start_position_handle_x, start_position_handle_y, start_position_handle_z;
    public double start_rotation_handle_x, start_rotation_handle_y, start_rotation_handle_z;
    public float rotate_z_prev_1 = 0;

    // Стартовая позиция резцедержки и продольной, поперечной рукояток
    public double start_position_tool_hold_x, start_position_tool_hold_y, start_position_tool_hold_z;
    public double current_position_tool_hold_x_1, current_position_tool_hold_z_1, current_position_tool_hold_x_2, current_position_tool_hold_z_2;
    public double start_rotation_handle_transverse_x, start_rotation_handle_transverse_y, start_rotation_handle_transverse_z;
    public float rotate_x_prev_2 = 0;

    public double start_position_handle_lateral_x, start_position_handle_lateral_y, start_position_handle_lateral_z;
    public double current_position_handle_lateral_x_1, current_position_handle_lateral_z_1, current_position_handle_lateral_x_2, current_position_handle_lateral_z_2;
    public double start_rotation_handle_lateral_x, start_rotation_handle_lateral_y, start_rotation_handle_lateral_z;
    public float rotate_z_prev_3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Задняя бабка
        start_position_tailstock_x = tailstock.transform.position.x;
        start_position_tailstock_y = tailstock.transform.position.y;
        start_position_tailstock_z = tailstock.transform.position.z;

        start_position_handle_x = handle.transform.position.x;
        start_position_handle_y = handle.transform.position.y;
        start_position_handle_z = handle.transform.position.z;

        start_rotation_handle_x = handle.transform.rotation.x;
        start_rotation_handle_y = handle.transform.rotation.y;
        start_rotation_handle_z = handle.transform.rotation.z;

        // Резцедержка
        start_position_tool_hold_x = tool_hold.transform.position.x;
        start_position_tool_hold_y = tool_hold.transform.position.y;
        start_position_tool_hold_z = tool_hold.transform.position.z;

        current_position_tool_hold_x_1 = tool_hold.transform.position.x;
        current_position_tool_hold_z_1 = tool_hold.transform.position.z;
        current_position_tool_hold_x_2 = tool_hold.transform.position.x;
        current_position_tool_hold_z_2 = tool_hold.transform.position.z;

        // Поперечная рукоятка резцедержки
        start_rotation_handle_transverse_x = handle_transverse.transform.rotation.x;
        start_rotation_handle_transverse_y = handle_transverse.transform.rotation.y;
        start_rotation_handle_transverse_z = handle_transverse.transform.rotation.z;

        // Продольная рукоятка резцедержки
        start_position_handle_lateral_x = handle_lateral.transform.position.x;
        start_position_handle_lateral_y = handle_lateral.transform.position.y;
        start_position_handle_lateral_z = handle_lateral.transform.position.z;

        current_position_handle_lateral_x_1 = handle_lateral.transform.position.x;
        current_position_handle_lateral_z_1 = handle_lateral.transform.position.z;
        current_position_handle_lateral_x_2 = handle_lateral.transform.position.x;
        current_position_handle_lateral_z_2 = handle_lateral.transform.position.z;

        start_rotation_handle_lateral_x = handle_lateral.transform.rotation.x;
        start_rotation_handle_lateral_y = handle_lateral.transform.rotation.y;
        start_rotation_handle_lateral_z = handle_lateral.transform.rotation.z;

    }

    // Update is called once per frame
    void Update()
    {

        /*// Вращение патрона
        float rotation_speed = 50;
        cartrige.transform.rotation = Quaternion.Slerp(cartrige.transform.rotation, Quaternion.Euler.eulerAngles(0, 0, (float)(100 * Time.deltaTime)), (float)(rotation_speed * Time.deltaTime));
        */

        // Показываем координату y нахождения задней бабки
        field.text = $"Позиция задней бабки: {Math.Round(start_position_tailstock_z - tailstock.transform.position.z, 3)} \nПозиция резцедержки по X: {Math.Round(start_position_tool_hold_x - tool_hold.transform.position.x, 3)} \nПозиция резцедержки по Z: {Math.Round(start_position_tool_hold_z - tool_hold.transform.position.z, 3)}";

        // Перемещение задней бабки по оси Z
        if (rotate_z_prev_1 < handle.transform.rotation.eulerAngles.z)
        {

            tailstock.transform.position = new Vector3((float)start_position_tailstock_x, (float)start_position_tailstock_y, (float)(start_position_tailstock_z - handle.transform.rotation.eulerAngles.z / 1000));
            handle.transform.position = new Vector3((float)start_position_handle_x, (float)start_position_handle_y, (float)(start_position_handle_z - handle.transform.rotation.eulerAngles.z / 1000));
            rotate_z_prev_1 = handle.transform.rotation.z;

        }

        // Перемещение резцедержки по оси X
        if (rotate_x_prev_2 != handle_transverse.transform.rotation.x)
        {

            tool_hold.transform.position = new Vector3((float)(current_position_tool_hold_x_2 + handle_transverse.transform.rotation.x / 10), (float)start_position_tool_hold_y, (float)current_position_tool_hold_z_2);
            handle_lateral.transform.position = new Vector3((float)(current_position_handle_lateral_x_2 + handle_transverse.transform.rotation.x / 10), (float)start_position_handle_lateral_y, (float)current_position_handle_lateral_z_2);

            // Запоминаем новые координаты
            current_position_tool_hold_x_1 = tool_hold.transform.position.x;
            current_position_tool_hold_z_1 = tool_hold.transform.position.z;
            current_position_handle_lateral_x_1 = handle_lateral.transform.position.x;
            current_position_handle_lateral_z_1 = handle_lateral.transform.position.z;

            rotate_x_prev_2 = handle_transverse.transform.rotation.x;

        }

        // Перемещение резцедержки по оси Z
        if (rotate_z_prev_3 != handle_lateral.transform.rotation.z)
        {

            tool_hold.transform.position = new Vector3((float)current_position_tool_hold_x_1, (float)start_position_tool_hold_y, (float)(current_position_tool_hold_z_1 + handle_lateral.transform.rotation.z / 10));
            handle_lateral.transform.position = new Vector3((float)current_position_handle_lateral_x_1, (float)start_position_handle_lateral_y, (float)(current_position_handle_lateral_z_1 + handle_lateral.transform.rotation.z / 10));

            // Запоминаем новые координаты
            current_position_tool_hold_x_2 = tool_hold.transform.position.x;
            current_position_tool_hold_z_2 = tool_hold.transform.position.z;
            current_position_handle_lateral_x_2 = handle_lateral.transform.position.x;
            current_position_handle_lateral_z_2 = handle_lateral.transform.position.z;

            rotate_z_prev_3 = handle_lateral.transform.rotation.z;

        }

    }

    // Обработка нажатия кнопки; Возвращаем заднюю бабку и резцедержку с их рукоятками в исходное положение
    public void ButtonClick()  
    {

        tailstock.transform.position = new Vector3((float)start_position_tailstock_x, (float)start_position_tailstock_y, (float)start_position_tailstock_z);
        handle.transform.position = new Vector3((float)start_position_handle_x, (float)start_position_handle_y, (float)start_position_handle_z);
        handle.transform.rotation = Quaternion.Euler((float)start_rotation_handle_x, (float)start_rotation_handle_y, (float)start_rotation_handle_z);

        tool_hold.transform.position = new Vector3((float)start_position_tool_hold_x, (float)start_position_tool_hold_y, (float)start_position_tool_hold_z);
        
        handle_transverse.transform.rotation = Quaternion.Euler((float)start_rotation_handle_transverse_x, (float)start_rotation_handle_transverse_y, (float)start_rotation_handle_transverse_z);

        handle_lateral.transform.position = new Vector3((float)start_position_handle_lateral_x, (float)start_position_handle_lateral_y, (float)start_position_handle_lateral_z);
        handle_lateral.transform.rotation = Quaternion.Euler((float)start_rotation_handle_lateral_x, (float)start_rotation_handle_lateral_y, (float)start_rotation_handle_lateral_z);

    }
}
