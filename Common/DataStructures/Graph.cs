using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2020.Common.DataStructures
{
    public class Graph<T>
    {
        public Graph()
        {
        }
    }

    public class Node<T>
    {
        public Node()
        {
            Neighbors = Enumerable.Empty<T>();
        }

        public T Value { get; init; }

        IEnumerable<T> Neighbors { get; init; }
    };
}
