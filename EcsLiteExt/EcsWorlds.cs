using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace EcsLiteExt
{
public sealed class EcsWorlds
{
	public static EcsWorlds I = new EcsWorlds();

	private List<EcsWorld> _worlds = new List<EcsWorld>();
	public int Count => _worlds.Count;
	public List<EcsWorld> Worlds => _worlds;

	public EcsWorld Add (EcsWorld ecsWorld, out int worldId)
	{
		worldId = _worlds.Count;
		_worlds.Add(ecsWorld);
		return ecsWorld;
	}

	public EcsWorld Get(Int32 worldId)
	{
		return _worlds[worldId];
	}
}
}