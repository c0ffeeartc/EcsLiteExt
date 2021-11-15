using Leopotam.EcsLite;

namespace EcsLiteExt
{
public static class EcsExtEntPool
{
	public static ref T Get<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompData
	{
		return ref pool.Get(entId);
	}

	public static ref T GetOrAdd<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompData
	{
		if (pool.Has(entId))
		{
			return ref pool.Get(entId);
		}
		return ref pool.Add(entId);
	}

	public static ref T Add<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompData
	{
		return ref pool.Add(entId);
	}

	public static void Del<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompData
	{
		pool.Del(entId);
	}

	public static bool Has<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompData
	{
		return pool.Has(entId);
	}

	public static bool Is<T>(this int entId, EcsPool<T> pool) where T : struct, IEcsCompFlag
	{
		return pool.Has(entId);
	}

	public static void Flag<T>(this int entId, EcsPool<T> pool, bool flag) where T : struct, IEcsCompFlag
	{
		var hasComponent = pool.Has(entId);

		if (flag)
		{
			if (hasComponent)
			{
				return;
			}
			pool.Add(entId);
		}
		else
		{
			if (!hasComponent)
			{
				return;
			}
			pool.Del(entId);
		}
	}
}
}