using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    // ENEMIES

    public const int ENEMY_TYPE_GREEN  = 1;
    public const int ENEMY_TYPE_BLUE   = 2;
    public const int ENEMY_TYPE_RED    = 3;
    
    // SCENES

    public static string S_SPLASH_SCREEN = "SplashScreen";
    public static string S_GAME = "Game";

    // TAGS

    public static string TAG_ENEMY  = "Enemy";
    public static string TAG_ENEMY_BULLET  = "EnemyBullet";
    public static string TAG_PLAYER = "Player";
    public static string TAG_BULLET = "Bullet";
    public static string TAG_GAME_OVER_COLLIDER = "GameOverCollider";

    // LAYOUTS

    public static int LAYOUT_1  = 1;
    public static int LAYOUT_2  = 2;

    // MENUS

    public static int MENU_MAIN     = 0;
    public static int MENU_CREDITS  = 1;
    public static int MENU_END_GAME = 2;

    // UX
    public static float FADE_TIME = 0.85f;
}
