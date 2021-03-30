using System.Collections.Generic;

namespace SpectraMaster.Data
{
    public class SpectraAnswer
    {
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string AnswerDescription { get; set; }
        public List<ProblemPicture> ProblemPictures { get; set; }
        public List<AnswerPicture> AnswerPictures { get; set; }

        public NMRProblemAnswer NmrProblemAnswer{ get; set; }
        public MassProblemAnswer MassProblemAnswer { get; set; }
    }
}