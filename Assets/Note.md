# 开发日志

## 脚本说明



#### 单元控制Unit

UnitInteraction：单元选中后显示光圈

UnitData：存放单元数据

UnitHp：控制单元的血条显示

UnitMotor：控制单元的移动

UnitInput：控制单元的输入



#### Managers

InputManager：统一管理输入



## 摄像机

摄像机属性：正交，边长20以上

摄像机最好使用正交模式，这样才好控制地图边缘和小地图的摄像机边框。当然使用透视也行，不过会麻烦很多，至少小地图上摄像机的框要用梯形之类的。



### 飞行物体的寻路

在空中增加一个NavMesh就行了



## 战争迷雾

1. 利用Projector投下黑色的阴影