using Leopotam.EcsLite;

namespace EcsLiteExt
{
public static class EcsExtEntWorldId
{
	public static ref T Get<T>(this int entId, int worldId) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		return ref pool.Get(entId);
	}

	public static ref T GetOrAdd<T>(this int entId, int worldId) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		return ref entId.GetOrAdd(pool);
	}

	public static ref T Add<T>(this int entId, int worldId) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		return ref pool.Add(entId);
	}

	public static void Del<T>(this int entId, int worldId) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		pool.Del(entId);
	}

	public static bool Has<T>(this int entId, int worldId) where T : struct, IEcsCompData
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		return pool.Has(entId);
	}

	public static void Flag<T>(this int entId, int worldId, bool flag) where T : struct, IEcsCompFlag
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		entId.Flag(pool, flag);
	}

	public static bool Is<T>(this int entId, int worldId) where T : struct, IEcsCompFlag
	{
		EcsPool<T> pool = EcsPools.I.Get<T>(worldId);
		return pool.Has(entId);
	}
}
}