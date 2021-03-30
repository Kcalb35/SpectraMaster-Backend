namespace SpectraMaster.Data
{
    public class NMRProblem
    {
        public int Id { get; set; }
        public short H { get; set; }
        public short C { get; set; }
        public short N { get; set; }
        public short O { get; set; }
        public short F { get; set; }
        public short Si { get; set; }
        public short P { get; set; }
        public short S { get; set; }
        public short Cl { get; set; }
        public short Br { get; set; }
        public short I { get; set; }
        public NMRProblemAnswer NmrProblemAnswer { get; set; }
    }
    
    public class MassProblem
    {
        public int Id { get; set; }
        public float IonPeak { get; set; }
        public MassProblemAnswer MassProblemAnswer{ get; set; }
    }
}