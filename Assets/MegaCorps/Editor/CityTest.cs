using NUnit.Framework;

[TestFixture]
[Category("City Tests")]
public class CityTest {

	[Test]
    public void testSingleton() {
        City city1 = City.getInstance();
        City city2 = City.getInstance();

        Assert.AreSame(city1, city2);
    }
}
