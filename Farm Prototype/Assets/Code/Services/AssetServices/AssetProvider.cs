﻿using UnityEngine;

namespace Code.Services.AssetServices
{
    public class AssetProvider : IAssetProvider
    {
        public T Instantiate<T>(string path, Vector3 at) where T : Object
        {
            var prefab = ResourceLoader.Load<T>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T Instantiate<T>(string path, Transform parent) where T : Object
        {
            var prefab = ResourceLoader.Load<T>(path);
            return Object.Instantiate(prefab, parent,false);
        }
        
        public T Instantiate<T>(string path,Vector3 pos, Transform parent) where T : Object
        {
            var prefab = ResourceLoader.Load<T>(path);
            return Object.Instantiate(prefab, pos, Quaternion.identity,parent);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            var prefab = ResourceLoader.Load<T>(path);
            return Object.Instantiate(prefab);
        }

        public T Load<T>(string path) where T : Object => 
            ResourceLoader.Load<T>(path);

        public T[] LoadAll<T>(string path) where T : Object => 
            ResourceLoader.LoadAll<T>(path);
    }
}