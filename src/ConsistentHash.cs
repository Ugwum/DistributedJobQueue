using System;
using System.Text;
using System.Security.Cryptography;

namespace ConsistentHashing
{
    public class ConsistentHash<T>
    {
        private readonly SortedDictionary<int, T> ring = new SortedDictionary<int, T>();
        private readonly int virtualNodes;

        public ConsistentHash(int virtualNodes = 10)
        {
            this.virtualNodes = virtualNodes;
        }

        public void AddNode(T node)
        {
            for (int i = 0; i < virtualNodes; i++)
            {
                string virtualNodeName = $"{node}-VNode-{i}";
                int hash = GetHash(virtualNodeName);
                ring[hash] = node;
            }
        }

        public void RemoveNode(T node)
        {
            for (int i = 0; i < virtualNodes; i++)
            {
                string virtualNodeName = $"{node}-VNode-{i}";
                int hash = GetHash(virtualNodeName);
                ring.Remove(hash);
            }
        }

        public T GetNode(string key)
        {
            if (ring.Count == 0)
            {
                throw new InvalidOperationException("No nodes in the ring");
            }

            int keyHash = GetHash(key);

            foreach (var node in ring)
            {
                if (node.Key >= keyHash)
                {
                    return node.Value;
                }
            }

            return ring.First().Value;
        }

        private int GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToInt32(hashBytes, 0);
            }
        }
    }
}