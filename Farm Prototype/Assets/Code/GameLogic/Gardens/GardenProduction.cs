using System;
using Code.Data.GardenData;
using Code.Data.ResourceData;
using Code.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.GameLogic.Gardens
{
    public enum ProductionState
    {
        WaitWatering,
        Growing,
        CompleteGrowth
    }

    public class GardenProduction : MonoBehaviour
    { 
        public event Action<float> GrowingChanged;
        public event Action<ProductionState> ProductionStateChanged;

        [SerializeField]
        private GameObject _cropsVisual;
        
        private IResourceService _resourceRepository;
        private GardenData _gardenData;

        private int _currentChanceDrop;

        private  int _percentDropSeed;
        private float _growing; 
        private float _growingTimer; 
        private float _growingTime; 
        private readonly float _defaultScale = 0f;
        private readonly float _finishProductScale = 1f;
        
        private Vector3 _growingScaleVector;
        
        private ResourceType _harvestingResourceType; 
        private ProductionState _productionState;
        public void Init(IResourceService resourceRepository, GardenData gardenData) 
        { 
            _resourceRepository = resourceRepository; 
            _gardenData = gardenData;
            
            _growingTime = _gardenData.TimeGrowing; 
            _percentDropSeed = _gardenData.DropData.SeedDropChance;
            
            SetCrops(_gardenData.colorCrops); 
        }

    private void Update()
    {
        if (_productionState == ProductionState.Growing && _growingTimer <= _growingTime)
            GrowingCycle();
    }

    public void Harvesting(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold:
                _resourceRepository.AddGold(_gardenData.DropData);
                break;
            case ResourceType.Seed:
                _resourceRepository.AddSeed(_gardenData.DropData);
                break;
        }

        SetLocalScaleHeight(_cropsVisual.transform, _defaultScale);
        SetProductionState(ProductionState.WaitWatering);
    }

    public GardenData GetGardenData() =>
        _gardenData;

    public ResourceType GetHarvestingResourceType() => 
        _harvestingResourceType;

    public ProductionState GetProductionState() => _productionState;

    private void SetCrops(Color color)
    {
        _cropsVisual.SetActive(true);
        SetProductionState(ProductionState.WaitWatering);
        
        MeshRenderer[] meshCrops = _cropsVisual.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshCrop in meshCrops)
            meshCrop.material.color = color;
    }

    public void Growing()
    {
        SetProductionState(ProductionState.Growing);
        
        _growingTimer = 0f;
    }

    private void GrowingCycle()
    {
        _growingTimer += Time.deltaTime;

        _growing = Mathf.Lerp(_defaultScale,_finishProductScale, _growingTimer/_growingTime);

        SetLocalScaleHeight(_cropsVisual.transform, _growing);
        GrowingChanged?.Invoke(_growing);
        
        if (_growing >= _finishProductScale)
            FinishGrowing();
    }

    private void SetLocalScaleHeight(Transform transformProduct,float valueY)
    {
        Vector3 localScaleProduct = transformProduct.localScale;
        localScaleProduct.y = valueY;

        transformProduct.localScale = localScaleProduct;
    }

    private void FinishGrowing()
    {
        _currentChanceDrop = Random.Range(0, 100);

        if (_currentChanceDrop <= _percentDropSeed)
        {
            _harvestingResourceType = ResourceType.Seed;
            SetProductionState(ProductionState.CompleteGrowth);
        }

        _harvestingResourceType = ResourceType.Gold;
        SetProductionState(ProductionState.CompleteGrowth);
    }

    private void SetProductionState(ProductionState state)
    {
        _productionState = state;
        ProductionStateChanged?.Invoke(_productionState);
    }
    }
}