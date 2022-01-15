namespace s5_zi_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Lab3Routine();
        }

        private static void Lab3Routine()
        {
            WorkExecutor lab3Executor = new(@"textfiles/english.txt", "Kasperovich", "Eugeniya");
            lab3Executor.ExecuteRoutine();
        }     
    }
}
