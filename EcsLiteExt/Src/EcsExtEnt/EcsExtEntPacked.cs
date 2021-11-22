using Leopotam.EcsLite;

namespace EcsLiteExt
{
public static class EcsExtEntPacked
{
	public static ref T Get<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		return ref pool.Get(entPacked.Id);
	}

	public static ref T GetOrAdd<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompData
	{
		return ref entPacked.Id.GetOrAdd<T>(entPacked.World.Id);
	}

	public static ref T Add<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		return ref pool.Add(entPacked.Id);
	}

	public static void Del<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		pool.Del(entPacked.Id);
	}

	public static bool Has<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		return pool.Has(entPacked.Id);
	}

	public static bool Is<T>(this ref EcsPackedEntityWithWorld entPacked) where T : struct, IEcsCompFlag
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		return pool.Has(entPacked.Id);
	}

	public static void Flag<T>(this ref EcsPackedEntityWithWorld entPacked, bool flag) where T : struct, IEcsCompFlag
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(entPacked.World.Id);
		entPacked.Id.Flag(pool, flag);
	}
}
}