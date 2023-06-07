namespace HandlelisteDomain
{
    //konvensjon sier at denne burde hete HandlelisteVare
    public class VareInstance   
    {
        public int VareId { get; set; }
        public Vare Vare { get; set; }
        public int HandlelisteId { get; set; }
        public Handleliste Handleliste { get; set; }
        public int Mengde { get; set;}
        public bool IsCollected { get; set; }
    }
}