namespace Graph
{
    /// <summary>
    /// Класс для представления ребра графа
    /// </summary>
    public class Edge
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Edge(int id, string from, string to)
        {
            Id = id;
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"({From}, {To})";
        }
    }
}
