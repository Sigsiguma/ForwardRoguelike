using UnityEngine;
using System.Collections.Generic;
using UniRx;

namespace ingame.stage {
    public class StageCreater : MonoBehaviour {

        [SerializeField]
        private GameObject[] tiled_prefab_;

        [SerializeField]
        private ingame.player.PlayerAction player_;

        private const float tile_size_ = 0.5f;
        private List<GameObject> tiled_list_;

        private void Awake() {
            tiled_list_ = new List<GameObject>();
        }

        private void Start() {
            StageInit();
            player_.PlayerMovedAsObservable
                   .Subscribe(_ => StageUpdate());
        }

        private void StageInit() {
            float min_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane)).y - tile_size_;
            float max_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane)).y + tile_size_;

            int tile_num = (int)((max_pos_y - min_pos_y) / tile_size_);

            for (int i = 0; i <= tile_num; ++i) {
                tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)],
                 new Vector3(0f, min_pos_y + tile_size_ * i, 0f), Quaternion.identity, transform));
            }
        }

        //移動するタイミングで一番したを消して、ってやれば良さそう?
        private void StageUpdate() {

            Destroy(tiled_list_[0]);
            tiled_list_.Remove(tiled_list_[0]);

            float next_tile_pos = tiled_list_[tiled_list_.Count - 1].transform.position.y + tile_size_;
            tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)], new Vector3(0f, next_tile_pos, 0f)
                            , Quaternion.identity, transform));
        }
    }
}