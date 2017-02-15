using UnityEngine;
using UnityEditor;

namespace editor {
    public sealed class TextureImporterSettting : AssetPostprocessor {

        public override int GetPostprocessOrder() {
            return 0;
        }

        private void OnPreprocessTexture() {
            TextureImporter texture_importer = assetImporter as TextureImporter;

            texture_importer.filterMode = FilterMode.Point;
        }
    }
}
