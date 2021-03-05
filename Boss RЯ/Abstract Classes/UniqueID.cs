namespace Boss_RЯ.Abstract_Classes
{
    public abstract class UniqueID
    {
        public static long id = default;

        [Newtonsoft.Json.JsonProperty("Identifier")]
        public long ID { get; set; } = ++id;
    }
}
