using Google.Protobuf.WellKnownTypes;

namespace Cerbos.Sdk.Builders
{
    public class AttributeValue
    {
        private Value V { get; }

        private AttributeValue(Value value) {
            V = value;
        }
        
        public static AttributeValue BoolValue(bool value)
        {
            return new AttributeValue(Value.ForBool(value));
        }

        public static AttributeValue NullValue()
        {
            return new AttributeValue(Value.ForNull());
        }
        
        public static AttributeValue DoubleValue(double value)
        {
            return new AttributeValue(Value.ForNumber(value));
        }
        
        public static AttributeValue StringValue(string value)
        {
            return new AttributeValue(Value.ForString(value));
        }

        public Value ToValue() {
            return V;
        }
    }
}