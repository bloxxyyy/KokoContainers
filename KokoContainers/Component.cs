namespace Koko.Containers;

public abstract class Component {
	public Entity Entity { get; set; }
	
	public static T Create<T>(Entity entity) where T : Component {
		var obj = (T)Activator.CreateInstance(typeof(T));
		obj.Entity = entity;
		BaseContainer.AddComponent(entity, obj);
		return obj;
	}
}