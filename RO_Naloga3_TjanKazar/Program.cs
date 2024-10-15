namespace RO_Naloga3_TjanKazar
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new startForm());
        }
    }
}
