# EcsLiteExt
Helpers for LeoEcsLite

## Installation
  - Copy `EcsLiteExt` sources folder to LeoEcsLite's folder with `asmdef` file
  - Modify LeoEcsLite
    - Make `EcsPackedEntityWithWorld.Id`, ``EcsPackedEntityWithWorld.World` public
    - Add `public int EcsWorld.Id;`
    - Add `EcsLiteExt.EcsWorlds.I.Add(this, out Id);` as first line to `public EcsWorld (in Config cfg = default) {`

## Examples
```csharp
    // has extension methods for int, EcsPackedEntityWithWorld etc.
    {
        int entInt = cmdWorld.NewEntity();

        ref CompA compA1 = ref entInt.Add<CompA>(cmdWorld.Id);
        ref CompA compA2 = ref entInt.GetOrAdd<CompA>(cmdWorld.Id);
        ref CompA compA3 = ref entInt.Get<CompA>(cmdWorld.Id);
        bool hasCompA = entInt.Has<CompA>(cmdWorld.Id);
        entInt.Del<CompA>(cmdWorld.Id);

        entInt.Flag<CompFlagA>(cmdWorld.Id, true);
        entInt.Is<CompFlagA>(cmdWorld.Id);
    }

public struct CompA : IEcsCompData
{
    public int Value;
}

public struct CompFlagA : IEcsCompFlag
{
}
```
