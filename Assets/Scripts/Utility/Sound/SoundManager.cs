using UnityEngine;
using System.Collections;

/*
 * 使い方
 * シーンのManagerがあればManager,なければそれに相当するもののスクリプトのAwakeにて
 * utility.sound.SoundManager.Init()
 * を呼んでください。
 * 
 *  再生するための各サウンドは、ResourcesフォルダのAudio/BGM or Audio/SEに入れてください
 * 
 *  hogeのBGMをボリューム0.8で再生
 *  utility.sound.SoundManager.BGM.Play(hoge, 0.8f);
 * 
 *  hogeのBGMを1.0秒でフェードアウトしつつ停止
 *  utility.sound.SoundManager.BGM.Stop(hoge, 1.0f);
 * 
 *  サウンドを全て停止
 *  utility.sound.SoundManager.BGM.StopAll();
 * 
 *  hogeのBGMをポーズ
 *  utility.sound.SoundManager.BGM.Pause(hoge);
 * 
 *  hogeのBGMをポーズから再生
 *  utility.sound.SoundManager.BGM.UnPause(hoge);
 * 
 *  サウンドを全てポーズ
 *  utility.sound.SoundManager.BGM.PauseAll();
 *  
 */
namespace utility.sound {
    public static class SoundManager {

        private static BGM bgm_ = null;
        private static SE se_ = null;

        public static BGM BGM { get { return bgm_; } }
        public static SE SE { get { return se_; } }

        public static void Init() {
            if (bgm_ == null && se_ == null) {
                bgm_ = new BGM();
                se_ = new SE();
                SoundResources.Init();
                SoundSystem.Init();
                Debug.Log("<color=green> SoundManager is initialized!!! </color>");
            } else {
                Debug.LogWarning("SoundManager has been initialized!!!!");
            }
        }

        public static void StopAll() {
            Debug.Assert(bgm_ != null, "Please Init SoundManager!");
            Debug.Assert(se_ != null, "Please Init SoundManager!!");

            bgm_.StopCategoryAll();
            se_.StopCategoryAll();
        }

        public static void PauseAll() {

            Debug.Assert(bgm_ != null, "Please Init SoundManager!");
            Debug.Assert(se_ != null, "Please Init SoundManager!!");

            bgm_.PauseCategoryAll();
            se_.PauseCategoryAll();
        }

        public static void UnPauseAll() {

            Debug.Assert(bgm_ != null, "Please Init SoundManager!");
            Debug.Assert(se_ != null, "Please Init SoundManager!!");

            bgm_.UnPauseCategoryAll();
            se_.UnPauseCategoryAll();
        }

        public static void Temp() {
            bgm_ = null;
            se_ = null;
            SoundSystem.Release();
        }
    }
}

