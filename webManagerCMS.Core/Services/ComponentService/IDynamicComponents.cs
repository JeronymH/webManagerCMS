namespace webManagerCMS.Core.Services.ComponentService
{
	public interface IDynamicComponents
	{
		IDictionary<string, Type> Components { get; }
	}
}
