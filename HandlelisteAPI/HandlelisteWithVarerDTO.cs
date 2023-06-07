namespace HandlelisteAPI
{
    public class HandlelisteWithVarerDTO
    {
        public HandlelisteWithVarerDTO()
        {
            Varer = new List<VareInstanceDTO>();
        }
        public int HandlelisteId { get; set; }
        public string UserId { get; set; }
        public string HandlelisteName { get; set; }
        public List<VareInstanceDTO> Varer { get; set; }
    }

    public class VareInstanceDTO
    {
        public int VareId { get; set; }
        public string VareName { get; set; }
        public int Mengde { get; set; }
        public bool IsCollected { get; set; }
    }
}
