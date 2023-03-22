using System;
using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.Services;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

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
        
        private IResourceService _resourceRepository;
        private GardenData _gardenData;
        private RowProducts _rowProducts;
        
        private int _currentChanceDrop;

    private readonly int _percentDropSeed = 20;

    private float _growing;
    private float _growingTimer;
    private float _growingTime;
    private readonly float _defaultScale = 0f;
    private readonly float _finishProductScale = 1f;

    private Vector3 _rowProductsLocalScale;
    private Vector3 _growingScaleVector;

    private ResourceType _harvestingResourceType;
    private ProductionState _productionState;

    public void Init(IResourceService resourceRepository, GardenData gardenData)
    {
        _resourceRepository = resourceRepository;
        _gardenData = gardenData;

        _growingTime = _gardenData.GeneratorData.TimeGrowingCrops;
    }

    private void Update()
    {
        if (_productionState == ProductionState.Growing && _growingTimer <= _growingTime)
            GrowingCycle(_rowProductsLocalScale);
    }

    public void SetRowProducts(RowProducts rowProducts)
    {
        _rowProducts = rowProducts;
        _rowProductsLocalScale = _rowProducts.transform.localScale;
        SetProductionState(ProductionState.WaitWatering);
    }

    public void Growing()
    {
        SetProductionState(ProductionState.Growing);
        
        _growingTimer = 0f;
    }

    private void GrowingCycle(Vector3 localScale)
    {
        _growingTimer += Time.deltaTime;

        _growing = Mathf.Lerp(_defaultScale,_finishProductScale, _growingTimer/_growingTime);

        SetLocalScaleHeight(_rowProducts.transform, _growing);
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

    public void Harvesting(ResourceType type)
    {
        _resourceRepository.AddResource(type,
            type == ResourceType.Gold ? _gardenData.GeneratorData.CoinAmout : _gardenData.GeneratorData.SeedAmount);
        
        SetLocalScaleHeight(_rowProducts.transform, _defaultScale);
        SetProductionState(ProductionState.WaitWatering);
    }

    public GardenData GetGardenData() =>
        _gardenData;

    public ResourceType GetHarvestingResourceType() => 
        _harvestingResourceType;
    }
}