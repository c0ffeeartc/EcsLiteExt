using Leopotam.EcsLite;

namespace EcsLiteExt.Api
{
public class EcsApi : IEcsApi
{
	public static IEcsApi I = new EcsApi();

	public EcsWorld NewWorld()
	{
		return EcsWorlds.I.Add(new EcsWorld(), out int id);
	}
}
}