namespace HandlelisteDomain
{
    public class Vare
    {
        public Vare() {
            VareInstancer = new List<VareInstance>();
        }
        public int VareId { get; set; }
        public string VareName { get; set;}
        public List<VareInstance> VareInstancer { get; set; }
    }
}