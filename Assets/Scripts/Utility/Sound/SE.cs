using UnityEngine;
using System.Collections;

namespace utility.sound {
    public class SE : SoundBase {

        public SE() {
            path_ = SoundSettings.se_path_;
            category_id_ = (int)SoundSettings.SoundCategory.SE;
        }

        public void Play(string file_name, float volume = 1.0f, bool loop = false) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), file_name + " is empty!!!");

            base.Play(file_name, loop, true, null, volume);
        }

        public void Play(string file_name, System.Action onFinish, float volume = 1.0f, bool loop = false) {

            Debug.Assert(!string.IsNullOrEmpty(file_name), file_name + " is empty!!!");

            base.Play(file_name, loop, true, onFinish, volume);
        }
    }

}
