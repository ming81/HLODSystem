using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Unity.HLODSystem.Utils
{
    public static class MeshUtils
    {
        public static MeshData WorkingObjectToMeshData(WorkingObject wo)
        {
            MeshData meshData = MeshData.CreateInstance<MeshData>();

            meshData.Mesh = wo.Mesh.ToMesh();

            for (int mi = 0; mi < wo.Materials.Count; ++mi)
            {
                WorkingMaterial wm = wo.Materials[mi];
                MeshData.MaterialData materialData = new MeshData.MaterialData();

                string[] textureNames = wm.GetTextureNames();

                materialData.Material = new Material(wm.ToMaterial());
                materialData.Textures = new List<MeshData.TextureData>();

                for (int ti = 0; ti < textureNames.Length; ++ti)
                {
                    WorkingTexture wt = wm.GetTexture(textureNames[ti]);
                    MeshData.TextureData textureData = new MeshData.TextureData();

                    Texture2D tex = wt.ToTexture();

                    textureData.Name = textureNames[ti];
                    textureData.Format = tex.graphicsFormat;
                    textureData.Width = tex.width;
                    textureData.Height = tex.height;
                    textureData.Bytes = tex.GetRawTextureData();
                    
                    materialData.Textures.Add(textureData);
                }
                
                meshData.AddMaterialData(materialData);
                
            }

            return meshData;
        }
        
    }
}