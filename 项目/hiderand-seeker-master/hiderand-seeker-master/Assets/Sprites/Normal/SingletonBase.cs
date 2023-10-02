public class Singleton<T> where T : class, new()
{
    private static T instance;

    // �ṩһ�������ľ�̬��������ȡ����ʵ��
    public static T Instance
    {
        get
        {
            // ���ʵ�������ڣ��򴴽�һ���µ�ʵ��
            if (instance == null)
            {
                instance = new T();
                
            }

            return instance;
        }
    }

    // ������������������ʵ������������
    // ���磺
    // public void SomeMethod() { /* ʵ�ַ����߼� */ }
    // public int SomeProperty { get; set; }
   
}
