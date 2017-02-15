/*
 * 使い方
 * ゲーム内のAwakeのどこかで、
 * utility.others.StringBuilder.Create();
 * を呼んでください
 * 
 *  文字列hogeとfooを連結
 *  utility.others.StringBuilder.Add(hoge,foo);
 */

namespace utility.others {
    public static class StringBuilder {
        private static System.Text.StringBuilder builder_ = null;

        public static bool Create() {
            if (builder_ != null) {
                return true;
            }

            builder_ = new System.Text.StringBuilder();

            return (builder_ != null) ? true : false;
        }

        public static bool Clear() {
            if (builder_ == null) {
                UnityEngine.Debug.LogError("builder is not create!!");
                return false;
            }

            builder_.Length = 0;

            return true;
        }

        public static string Add(params string[] str) {
            if (builder_ == null || str == null) {
                UnityEngine.Debug.LogError("builder is not create!!");
                return null;
            }

            int len = str.Length;

            if (len == 0) {
                return null;
            } else if (len == 1) {
                return str[0];
            }

            builder_.Length = 0;

            for (int i = 0; i < len; ++i) {
                builder_.Append(str[i]);
            }

            return builder_.ToString();
        }
    }
}

