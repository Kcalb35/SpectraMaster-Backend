using System.Collections.Generic;

namespace SpectraMaster.Data
{
    public class SpectraAnswer
    {
        public SpectraAnswer(string prob ,string ans,IEnumerable<ProblemPicture> probPics,IEnumerable<AnswerPicture> ansPics)
        {
            ProblemDescription = prob;
            AnswerDescription = ans;
            ProblemPictures = new List<ProblemPicture>();
            ProblemPictures.AddRange(probPics);
            AnswerPictures = new List<AnswerPicture>();
            AnswerPictures.AddRange(ansPics);
        }

        public SpectraAnswer()
        {
            
        } 
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string AnswerDescription { get; set; }
        public List<ProblemPicture> ProblemPictures { get; set; }
        public List<AnswerPicture> AnswerPictures { get; set; }

        public NMRProblemAnswer NmrProblemAnswer{ get; set; }
        public MassProblemAnswer MassProblemAnswer { get; set; }
    }
}