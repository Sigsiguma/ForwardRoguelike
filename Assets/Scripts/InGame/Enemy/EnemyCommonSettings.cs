using UnityEngine;

namespace ingame.enemy {
    public static class EnemyCommonSettings {
        public static readonly float ray_length_ = 0.3f;
        public static readonly float action_speed_ = 0.2f;
        public static readonly Vector2 left_down_ = new Vector2(-0.5f, -0.5f);
        public static readonly Vector2 front_ = new Vector2(0f, -0.5f);
        public static readonly Vector2 right_down_ = new Vector2(0.5f, -0.5f);
        public static readonly int player_mask_ = LayerMask.GetMask(new string[] { "Player" });
        public static readonly int obstacle_mask_ = LayerMask.GetMask(new string[] { "Wall", "Enemy" });
    }

    public enum ActionDir {
        None,
        Left,
        Front,
        Right
    };
}