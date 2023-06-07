namespace HandlelisteDomain
{
    public class Handleliste
    {
        public Handleliste() {
            Varer = new List<VareInstance>();
        }
        public int HandlelisteId { get; set; }
        public string UserId { get; set; }
        public string HandlelisteName { get; set; }
        public List<VareInstance> Varer { get; set; }
    }
}