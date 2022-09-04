using Koko.Containers;

namespace TestAppForKokoContainers;
internal class TestContainer : Container {
	public override void UpdateList() {
		Entity[] Entities = BaseContainer.GetAllEntities();

		foreach (var entity in Entities) {
			var components = BaseContainer.GetAllComponentsByEntity(entity);
			if (components.ContainsKey(typeof(TestComponent))) {
				ContainedItems.Add(entity);
			}
		}
	}
}
