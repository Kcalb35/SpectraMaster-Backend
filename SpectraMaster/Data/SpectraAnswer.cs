using System.Collections.Generic;

namespace SpectraMaster.Data
{
    public class SpectraAnswer
    {
        public SpectraAnswer(string prob ,string ans)
        {
            ProblemDescription = prob;
            AnswerDescription = ans;
        }

        public SpectraAnswer()
        {
            
        } 
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string AnswerDescription { get; set; }
        public List<ProblemPicture> ProblemPictures { get; set; }
        public List<AnswerPicture> AnswerPictures { get; set; }

        public MassProblem MassProblem { get; set; }
        public NMRProblem NmrProblem { get; set; }
    }
}