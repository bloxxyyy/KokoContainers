namespace Koko.Containers;

public class BaseContainer {

	/// <summary>
	/// The Base Container is the base container for all other Containers.
	/// </summary>
	public Dictionary<Entity, ObservableDictionary<Type, Component, Entity>> ComponentsDictByEntity = new();

	public static Entity[] GetAllEntities() {
		return EC_Container.ComponentsDictByEntity.Keys.ToArray();
	}

	public ObservableDictionary<Type, dynamic> Containers = new();

	private static BaseContainer EC_Container = new();

	public static ObservableDictionary<Type, Component, Entity> GetAllComponentsByEntity(Entity entity) {
		return EC_Container.ComponentsDictByEntity[entity];
	}

	public static ObservableDictionary<Type, dynamic> GetContainers() {
		return EC_Container.Containers;
	}

	internal static void AddContainer<Type>(Type container) {
		EC_Container.Containers.KeyValuePairChanged += EC_Container.Containers_KeyValuePairChanged;
		EC_Container.Containers.Add(typeof(Type), container);
		EC_Container.Containers.KeyValuePairChanged -= EC_Container.Containers_KeyValuePairChanged;
	}
	
	private BaseContainer() {
		ComponentsDictByEntity = new Dictionary<Entity, ObservableDictionary<Type, Component, Entity>>();
	}

	/// <summary>
	/// Adds a component to an entity.
	/// This should be called by components so they can add themselfs to the baselist.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <param name="component"></param>
	internal static void AddComponent<T>(Entity entity, T component) where T : Component {
		if (!EC_Container.ComponentsDictByEntity.ContainsKey(entity)) {
			EC_Container.ComponentsDictByEntity.Add(entity, new ObservableDictionary<Type, Component, Entity>());
		}

		EC_Container.ComponentsDictByEntity[entity].Add(typeof(T), component, entity);
		EC_Container.ComponentsDictByEntity[entity].KeyValuePairChanged += EC_Container.BaseContainer_KeyValuePairChanged;
	}

	private void BaseContainer_KeyValuePairChanged(object sender, KeyValuePairEventArgs<Type, Component, Entity> e) {
		if (e.Action == KeyValueChangedAction.Add) {

			return;
		}

		if (e.Action == KeyValueChangedAction.Remove) {

			return;
		}

		if (e.Action == KeyValueChangedAction.Clear) {
			ComponentsDictByEntity.Remove(e.Parent);
			return;
		}
	}
	
	private void Containers_KeyValuePairChanged(object sender, KeyValuePairEventArgs<Type, dynamic> e) {
		if (e.Action == KeyValueChangedAction.Add) {
			// this should only fire on the start of the project where containers are made.
			var container = (Container)e.KeyValuePair.Value;
			container.UpdateList();
			return;
		}

		if (e.Action == KeyValueChangedAction.Remove) {
			// We should never remove containers.
			return;
		}

		if (e.Action == KeyValueChangedAction.Clear) {
			// We should never clear containers.
			return;
		}
	}
}
