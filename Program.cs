namespace Graph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new BipartiteGraphA34();
            graph.Display();

            // Демонстрация дополнительных методов
            Console.WriteLine("\nДОПОЛНИТЕЛЬНЫЕ ВОЗМОЖНОСТИ ");
            Console.WriteLine(new string('-', 50));

            // Пример получения соседей
            string testVertex = "U1";
            var neighbors = graph.GetNeighbors(testVertex);
            Console.WriteLine($"\nСоседи вершины {testVertex}: {string.Join(", ", neighbors)}");

            // Пример проверки наличия ребра
            string v1 = "U3", v2 = "V3";
            bool hasEdge = graph.HasEdge(v1, v2);
            Console.WriteLine($"Ребро между {v1} и {v2}: {(hasEdge ? "существует" : "не существует")}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
