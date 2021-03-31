using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpectraMaster.Models;
using SpectraMaster.Utils;

namespace SpectraMaster.Controllers
{
    [ApiController]
    [Route("api")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _repo;
        private IFileUtil _fileUtil;

        public AnswerController(IAnswerRepository repo, IFileUtil fileUtil)
        {
            _repo = repo;
            _fileUtil = fileUtil;
        }

        [HttpGet("ans/{id:int}")]
        public IActionResult GetAnswerById([FromRoute] int id)
        {
            var ans = _repo.RetrieveAnswerById(id);
            if (ans == null) return NotFound();
            return Ok(ResponseUtil.AnswerConvert(ans));
        }

        [HttpGet("ans/all")]
        public IActionResult GetAllAnswer()
        {
            var ans = _repo.RetrieveAllAnswers();
            var resp = ans.Select(ResponseUtil.AnswerConvert);
            return Ok(resp);
        }

        [HttpGet("nmr")]
        public IActionResult GetAllNMR()
        {
            var probs = _repo.RetrieveAllNmrProblems().Select(ResponseUtil.NmrProblemConvert);
            return Ok(probs);
        }

        [HttpGet("nmr/{id:int}")]
        public IActionResult GetNMRById(int id)
        {
            var prob = _repo.RetrieveNmrProblemById(id);
            if (prob == null) return NotFound();
            return Ok(ResponseUtil.NmrProblemConvert(prob));
        }

        [HttpGet("mass")]
        public IActionResult GetAllMass()
        {
            var probs = _repo.RetrieveAllMassProblems().Select(ResponseUtil.MassProblemConvert);
            return Ok(probs);
        }

        [HttpGet("mass/{id:int}")]
        public IActionResult GetMassById(int id)
        {
            var prob = _repo.RetrieveMassProblemById(id);
            if (prob == null) return NotFound();
            return Ok(prob);
        }

        [HttpPost("ans/search")]
        public IActionResult SearchAnswer([FromBody] SearchReq req)
        {
            var answers = _repo.RetrieveAnswer(req.Formula, req.MinIonPeak, req.MaxIonPeak);
            if (answers == null || !answers.Any()) return NotFound();
            return Ok(answers.Select(ResponseUtil.AnswerConvert));
        }


        [HttpPost("ans")]
        public async Task<IActionResult> CreateAnswer([FromForm] CreateReq req)
        {
            if ((req.Formula==null || !req.Formula.IsValid()) && req.IonPeak< 0) return BadRequest();
            if (string.IsNullOrEmpty(req.Answer) || string.IsNullOrEmpty(req.Problem)) return BadRequest();
            // save files
            var problemPictures = new List<string>();
            var ansPictures = new List<string>();
            if (req.ProbFiles!= null && req.ProbFiles.Any())
                foreach (var pic in req.ProbFiles)
                    problemPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            if (req.AnsFiles!= null && req.AnsFiles.Any())
                foreach (var pic in req.AnsFiles)
                    ansPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            var answer = await _repo.CreateAnswerAsync(req.Problem, req.Answer, problemPictures, ansPictures, req.Formula, req.IonPeak);
            return Ok(ResponseUtil.AnswerConvert(answer));
        }

        [HttpDelete("ans/{id:int}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            var ans = await _repo.DeleteAnswerById(id);
            if (ans) return Ok();
            return NotFound();
        }

        [HttpDelete("nmr/{id:int}")]
        public async Task<IActionResult> DeleteNmrProblem(int id)
        {
            var flag = await _repo.DeleteNmrProblemById(id);
            if (flag) return Ok();
            return NotFound();
        }

        [HttpDelete("mass/{id:int}")]
        public async Task<IActionResult> DeleteMassProblem(int id)
        {
            var flag = await _repo.DeleteMassProblemById(id);
            if (flag) return Ok();
            return NotFound();
        }

        [HttpPut("ans/{id:int}")]
        public async Task<IActionResult> UpdateAnswer([FromRoute] int id, [FromForm] CreateReq req)
        {
            var ans = _repo.RetrieveAnswerById(id);
            if (ans == null) return NotFound();
            if ((req.Formula == null || !req.Formula.IsValid()) && req.IonPeak < 0) return BadRequest();
            if (string.IsNullOrEmpty(req.Answer) || string.IsNullOrEmpty(req.Problem)) return BadRequest();
            var problemPictures = new List<string>();
            var ansPictures = new List<string>();
            if (req.ProbFiles!= null && req.ProbFiles.Any())
                foreach (var pic in req.ProbFiles)
                    problemPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            if (req.AnsFiles!= null && req.AnsFiles.Any())
                foreach (var pic in req.AnsFiles)
                    ansPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            var answer = await _repo.UpdateAnswerAsync(id, req.Problem, req.Answer, problemPictures, ansPictures, req.IonPeak,
                req.Formula);
            return Ok(ResponseUtil.AnswerConvert(answer));
        }
    }
}