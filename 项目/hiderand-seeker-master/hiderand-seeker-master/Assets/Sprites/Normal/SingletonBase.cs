public class Singleton<T> where T : class, new()
{
    private static T instance;

    // 提供一个公共的静态方法来获取单例实例
    public static T Instance
    {
        get
        {
            // 如果实例不存在，则创建一个新的实例
            if (instance == null)
            {
                instance = new T();
                
            }

            return instance;
        }
    }

    // 在这里可以添加其他的实例方法和属性
    // 例如：
    // public void SomeMethod() { /* 实现方法逻辑 */ }
    // public int SomeProperty { get; set; }
   
}
