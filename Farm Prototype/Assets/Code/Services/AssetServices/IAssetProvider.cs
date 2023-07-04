﻿using UnityEngine;

namespace Code.Services.AssetServices
{
    public interface IAssetProvider
    {
        public T Instantiate<T>(string path, Vector3 at) where T : Object;
        public T Instantiate<T>(string path, Transform parent) where T : Object;
        public T Instantiate<T>(string path) where T : Object;

        public T Instantiate<T>(string path, Vector3 pos, Transform parent) where T : Object;
        public T Load<T>(string path) where T : Object;
        public T[] LoadAll<T>(string path) where T : Object;
    }
}