using UnityEngine;
using System.Collections;

namespace utility.sound {
    public class BGM : SoundBase {

        public BGM() {
            path_ = SoundSettings.bgm_path_;
            category_id_ = (int)SoundSettings.SoundCategory.BGM;
        }

        public void Play(string file_name, float volume = 1.0f, bool loop = true) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            if (SoundSystem.BGMContain(file_name)) {
                Debug.LogWarning("This BGM already played!!!!");
                return;
            }

            base.Play(file_name, loop, false, null, volume);
        }

        public void Play(string file_name, System.Action onFinish, float volume = 1.0f, bool loop = true) {
            Debug.Assert(!string.IsNullOrEmpty(file_name), "filename is empty!!!!");

            base.Play(file_name, loop, false, onFinish, volume);
        }
    }

}
