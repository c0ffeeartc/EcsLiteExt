using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using EcsLite.Custom;
using Leopotam.EcsLite;

namespace EcsLiteExt
{
public class EcsPools
{
    public static EcsPools I = new EcsPools();
    public int CompCount { get; private set; }
    private IEcsPool[] _pools;
    private List<Type> _compTypes = new List<Type>();
    private EcsWorlds _ecsWorlds;

    public void InitComps(EcsWorlds ecsWorlds)
    {
        _ecsWorlds = ecsWorlds;
        var type = typeof(IEcsComp);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));

        Type lookupT = typeof(Lookup<>);
        foreach ( Type t in types)
        {
            if (!t.IsValueType)
            {
                continue;
            }

            Type lookupType = lookupT.MakeGenericType(t);
            var fieldInfo = lookupType.GetField("Id" ,
                BindingFlags.Static
                | BindingFlags.SetField
                | BindingFlags.Public
                );
           
            if (fieldInfo == null)
            {
                throw new Exception(string.Format("Type `{0}' does not contains `Id' field", t.Name));
            } 
            fieldInfo.SetValue(null,_compTypes.Count);
            _compTypes.Add(t);
        }
        CompCount = _compTypes.Count();
        _pools = new IEcsPool[CompCount*_ecsWorlds.Count];
    }

    public EcsPool<TComp> Get<TComp>(int worldId) where TComp : struct
    {
        Int32 indexComp = Lookup<TComp>.Id;
        Int32 index = indexComp + worldId * indexComp;

        IEcsPool pool = _pools[index];
        if ( pool != null )
        {
            return Unsafe.As<EcsPool<TComp>>(pool);
        }

        EcsWorld world = _ecsWorlds.Get(worldId);
        EcsPool<TComp> newPool = world.GetPool<TComp>();
        _pools[index] = newPool;
        return newPool;
    }
}
}