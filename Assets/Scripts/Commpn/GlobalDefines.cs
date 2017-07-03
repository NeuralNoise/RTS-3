using UnityEngine;
using System.Collections;

public class GlobalDefines {
    public const int MAX_HP = 100;
    public const float DOUBLE_CLICK_GAP = 0.5f; //两次点击要在这个时间内才算双击
    public const float MIN_ERROR_RANGE = 0.0001f; //可接受的误差值范围

    //Tags
    public const string PLAYER_TAG = "Player";
    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string ENEMY_TAG = "Enemy";
    public const string BUILDING_TAG = "Building";

    //Layers
    public const string GROUND_LAYER = "Ground";
}
