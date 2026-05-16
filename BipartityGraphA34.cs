namespace Graph
{
    public class BipartiteGraphA34
    {
        // Перечисление множеств (доли графа)
        public string[] LeftPartition { get; private set; }  // Множество U
        public string[] RightPartition { get; private set; } // Множество V
        public string[] AllVertices { get; private set; }    // Объединённый список для индексации

        // Матрица смежности (размер 7x7)
        public int[,] AdjacencyMatrix { get; private set; }

        // Матрица инцидентности (размер 7x5)
        public int[,] IncidenceMatrix { get; private set; }

        // Перечень рёбер
        public List<Edge> EdgeList { get; private set; }

        // Векторы смежности (для каждой вершины список соседних)
        public Dictionary<string, List<string>> AdjacencyVectors { get; private set; }

        public BipartiteGraphA34()
        {
            LeftPartition = new[] { "U1", "U2", "U3" };
            RightPartition = new[] { "V1", "V2", "V3", "V4" };

            AllVertices = new[] { "U1", "U2", "U3", "V1", "V2", "V3", "V4" };

            EdgeList = new List<Edge>();
            AdjacencyVectors = new Dictionary<string, List<string>>();

            // Инициализация векторов смежности для всех вершин
            foreach (var vertex in AllVertices)
            {
                AdjacencyVectors[vertex] = new List<string>();
            }

            int vertexCount = AllVertices.Length; // 7
            int edgeCount = 5;

            AdjacencyMatrix = new int[vertexCount, vertexCount];
            IncidenceMatrix = new int[vertexCount, edgeCount];

            Populate();
        }

        /// <summary>
        /// Заполнение всех структур данных на основе 5 заданных рёбер
        /// </summary>
        private void Populate()
        {
            // Рёбра графа (индексы в массиве AllVertices)
            // U1=0, U2=1, U3=2, V1=3, V2=4, V3=5, V4=6
            var edges = new (int uIdx, int vIdx, string uName, string vName)[]
            {
                (0, 3, "U1", "V1"), // Ребро 1
                (0, 4, "U1", "V2"), // Ребро 2
                (1, 5, "U2", "V3"), // Ребро 3
                (2, 5, "U3", "V3"), // Ребро 4
                (2, 6, "U3", "V4")  // Ребро 5
            };

            for (int e = 0; e < edges.Length; e++)
            {
                int uIdx = edges[e].uIdx;
                int vIdx = edges[e].vIdx;
                string uName = edges[e].uName;
                string vName = edges[e].vName;

                // Заполняем перечень рёбер
                EdgeList.Add(new Edge(e + 1, uName, vName));

                // Заполняем матрицу смежности (симметричная)
                AdjacencyMatrix[uIdx, vIdx] = 1;
                AdjacencyMatrix[vIdx, uIdx] = 1;

                // Заполняем матрицу инцидентности
                IncidenceMatrix[uIdx, e] = 1;
                IncidenceMatrix[vIdx, e] = 1;

                // Заполняем векторы смежности
                AdjacencyVectors[uName].Add(vName);
                AdjacencyVectors[vName].Add(uName);
            }
        }

        /// <summary>
        /// Вывод всех структур данных в консоль
        /// </summary>
        public void Display()
        {
            Console.WriteLine("Двудольный граф A(3,4) с 5 рёбрами\n");

            //Перечисление множеств
            Console.WriteLine("1. ПЕРЕЧИСЛЕНИЕ МНОЖЕСТВ:");
            Console.WriteLine("   " + new string('-', 50));
            Console.WriteLine($"   Левая доля U:  {{ {string.Join(", ", LeftPartition)} }}");
            Console.WriteLine($"   Правая доля V: {{ {string.Join(", ", RightPartition)} }}");
            Console.WriteLine($"   Всего вершин: {AllVertices.Length}");
            Console.WriteLine();

            //Перечень рёбер
            Console.WriteLine("2. ПЕРЕЧЕНЬ РЁБЕР:");
            Console.WriteLine("   " + new string('-', 50));
            foreach (var edge in EdgeList)
            {
                Console.WriteLine($"   Ребро {edge.Id}: {edge.From} —— {edge.To}");
            }
            Console.WriteLine($"   Всего рёбер: {EdgeList.Count}");
            Console.WriteLine();

            //Векторы смежности
            Console.WriteLine("3. ВЕКТОРЫ СМЕЖНОСТИ:");
            Console.WriteLine("   " + new string('-', 50));
            foreach (var vertex in AllVertices)
            {
                var neighbors = AdjacencyVectors[vertex];
                string neighborsStr = neighbors.Count > 0
                    ? string.Join(", ", neighbors)
                    : "нет соседей";
                Console.WriteLine($"   {vertex}: [{neighborsStr}]");
            }
            Console.WriteLine();

            //Матрица смежности
            Console.WriteLine("4. МАТРИЦА СМЕЖНОСТИ (7×7):");
            Console.WriteLine("   " + new string('-', 50));
            Console.Write("       ");
            for (int j = 0; j < AllVertices.Length; j++)
                Console.Write($"{AllVertices[j],4}");
            Console.WriteLine();

            for (int i = 0; i < AllVertices.Length; i++)
            {
                Console.Write($"   {AllVertices[i],4} ");
                for (int j = 0; j < AllVertices.Length; j++)
                {
                    Console.Write($"{AdjacencyMatrix[i, j],4}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Матрица инцидентности
            Console.WriteLine("5. МАТРИЦА ИНЦИДЕНТНОСТИ (7 вершин × 5 рёбер):");
            Console.WriteLine("   " + new string('-', 50));
            Console.Write("       ");
            for (int j = 0; j < 5; j++)
                Console.Write($" E{j + 1}");
            Console.WriteLine();

            for (int i = 0; i < AllVertices.Length; i++)
            {
                Console.Write($"   {AllVertices[i],4} ");
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{IncidenceMatrix[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Получить соседей конкретной вершины
        /// </summary>
        public List<string> GetNeighbors(string vertex)
        {
            return AdjacencyVectors.ContainsKey(vertex)
                ? AdjacencyVectors[vertex]
                : new List<string>();
        }

        /// <summary>
        /// Проверка наличия ребра между двумя вершинами
        /// </summary>
        public bool HasEdge(string u, string v)
        {
            return AdjacencyVectors[u].Contains(v);
        }
    }
}
