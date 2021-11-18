# EcsLiteExt
WIP extension methods and other helpers for [LeoEcsLite](https://github.com/Leopotam/ecslite)

## Installation
> Warning: current installation doesn't work with Unity Package Manager, because it involves changing ecslite sources, and UPM will override these changes on each run. This will get fixed in next updates
  - Copy `EcsLiteExt` sources folder to LeoEcsLite's folder with `asmdef` file
  - Modify LeoEcsLite
    - Make `EcsPackedEntityWithWorld.Id`, `EcsPackedEntityWithWorld.World` public
    - Add `public int EcsWorld.Id;`
    - Add `EcsLiteExt.EcsWorlds.I.Add(this, out Id);` as first line to `public EcsWorld (in Config cfg = default) {`

## Initialization

At start of Init stage:

```csharp
// create all worlds beforehand
var cmdWorld = new EcsWorld();
var gameWorld = new EcsWorld();

// then run InitComps
EcsPools.I.InitComps(EcsWorlds.I);
```

## Usage
EcsLiteExt provides various extension methods to `int`, `EcsPackedEntityWithWorld`, etc.

### Components
`IEcsCompData` provides `Get<T>`, `Add<T>`, `GetOrAdd<T>`, `Has<T>`, `Del<T>` entity methods
```csharp
public struct CompA : IEcsCompData
{
    public int Value;
}
```

`IEcsCompFlag` provides `Is<T>`, `Flag<T>` entity methods
```csharp
public struct CompFlagA : IEcsCompFlag { }
```

### int Entity
```csharp
int entity = cmdWorld.NewEntity();

ref CompA compA = ref entity.Add<CompA>(cmdWorld.Id);
compA           = ref entity.Get<CompA>(cmdWorld.Id);
compA           = ref entity.GetOrAdd<CompA>(cmdWorld.Id);
bool hasCompA = entity.Has<CompA>(cmdWorld.Id);
entity.Del<CompA>(cmdWorld.Id);

entity.Flag<CompFlagA>(cmdWorld.Id, true);
bool isCompFlagA = entity.Is<CompFlagA>(cmdWorld.Id);
```

### EcsPackedEntityWithWorld
```csharp
var entPacked = cmdWorld.PackEntityWithWorld(cmdWorld.NewEntity());

if (entPacked.Unpack(out var world, out var entity))
{
    ref CompA compA = ref entPacked.Add<CompA>();
    compA           = ref entPacked.Get<CompA>();
    compA           = ref entPacked.GetOrAdd<CompA>();
    bool hasCompA = entPacked.Has<CompA>();
    entPacked.Del<CompA>();

    entPacked.Flag<CompFlagA>(true);
    bool isCompFlagA = entPacked.Is<CompFlagA>();
}
```

## Tradeoffs and Limitations
  - To provide API current implementation stores an array of `worldInstanceCount * componentTypeCount` references to `EcsPool` instances. This may be not optimal for very big or dynamic amount of worlds.

  - If only 1 world instance is expected API could be improved. Current implementation favours multiple worlds.

## Known Issues
  - IL2CPP builds are expected to have runtime exception `EcsPools.I.InitComps(EcsWorlds.I);` due to code stripping, if any component isn't used in code. Should be fixed soon by catching exception and continue execution
