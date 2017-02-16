using UnityEngine;
using System.Collections.Generic;

namespace ingame.stage {
    public class StageCreater : MonoBehaviour {

        [SerializeField]
        private GameObject[] tiled_prefab_;

        private float tile_size = 0.5f;
        private List<GameObject> tiled_list_;

        private void Awake() {
            tiled_list_ = new List<GameObject>();
        }

        private void Start() {
            StageInit();
        }

        private void Update() {
            StageUpdate();
        }

        private void StageInit() {
            float min_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane)).y;
            float max_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane)).y;

            int tile_num = (int)((max_pos_y - min_pos_y) / tile_size);

            for (int i = 0; i <= tile_num; ++i) {
                tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)],
                 new Vector3(0f, min_pos_y + tile_size * i, 0f), Quaternion.identity, transform));
            }
        }

        //移動するタイミングで一番したを消して、ってやれば良さそう?
        private void StageUpdate() {
            float min_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane)).y;
            float max_pos_y = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane)).y;

            for (int i = tiled_list_.Count - 1; i >= 0; --i) {
                if (tiled_list_[i].transform.position.y < min_pos_y) {
                    Destroy(tiled_list_[i]);
                    tiled_list_.Remove(tiled_list_[i]);
                    tiled_list_.Add(Instantiate(tiled_prefab_[Random.Range(0, tiled_prefab_.Length)],
                    new Vector3(0f, max_pos_y, 0f), Quaternion.identity, transform));
                }
            }
        }
    }
}