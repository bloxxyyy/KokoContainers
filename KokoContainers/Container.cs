namespace Koko.Containers;

/// <summary>
/// Container static create function should add itself to the container repository.
/// </summary>
public abstract class Container {
	public dynamic ContainedItems { set; get; }
	
	public static void Create<T1, TValue>() where T1 : Container where TValue : new() {
		var obj = (T1)Activator.CreateInstance(typeof(T1));
		obj.ContainedItems = new TValue();
		BaseContainer.AddContainer(obj);
	}

	public abstract void UpdateList();
}