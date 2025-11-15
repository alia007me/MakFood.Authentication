namespace MakFood.FBI.Utils
{
    public class JwsLocalOptions
    {
        public JwsLocalOptions() { }
        public JwsLocalOptions(string key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Expires { get; set; }
    }

}
