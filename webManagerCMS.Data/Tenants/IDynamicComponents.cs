namespace webManagerCMS.Data.Tenants {

    public interface IDynamicComponents
    {
		IDictionary<string, Type> Components { get; }
	}
}
