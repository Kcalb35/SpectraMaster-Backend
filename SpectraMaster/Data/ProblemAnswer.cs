namespace SpectraMaster.Data
{
    public class MassProblemAnswer
    {
        public int AnswerId { get; set; }
        public int MassProblemId { get; set; }
        public MassProblem MassProblem { get; set; }
        public SpectraAnswer Answer { get; set; }
    }

    public class NMRProblemAnswer
    {
        public int NMRProblemId { get; set; }
        public int AnswerId { get; set; }
        public NMRProblem NmrProblem { get; set; }
        public SpectraAnswer Answer { get; set; }
    }
}