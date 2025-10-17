namespace Cerbos.Sdk.Utility
{
    public static class Metadata
    {
        public static Grpc.Core.Metadata Merge(Grpc.Core.Metadata first, Grpc.Core.Metadata second)
        {
            if (first == null && second == null)
            {
                return null;
            }

            if (first != null && second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            Grpc.Core.Metadata combined = new Grpc.Core.Metadata();
            foreach (var m in first)
            {
                combined.Add(m);
            }

            foreach (var m in second)
            {
                combined.Add(m);
            }

            return combined;
        }
    }
}