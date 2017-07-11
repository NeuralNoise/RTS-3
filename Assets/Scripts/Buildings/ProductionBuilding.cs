using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 生产型建筑
/// </summary>
public class ProductionBuilding : BaseBuilding
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void InitData()
    {
        mData.Add(1001);
    }
}
