using Koko.Containers;
using TestAppForKokoContainers;

var entity = new Entity();
entity.Id = "TestEntity";
Component.Create<TestComponent>(entity);
Component.Create<TestSecondComponent>(entity);

var components = BaseContainer.GetAllComponentsByEntity(entity);

Container.Create<TestContainer, List<Entity>>();
BaseContainer.GetContainers().TryGetValue(typeof(TestContainer), out dynamic testContainer);
var bla = (List<Entity>)testContainer.ContainedItems;
components.Clear();
var x = 1;
