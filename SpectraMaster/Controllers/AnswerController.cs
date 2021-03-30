using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpectraMaster.Models;

namespace SpectraMaster.Controllers
{
    [ApiController]
    [Route("api/ans")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _repo;

        public AnswerController(IAnswerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAnswerById([FromRoute] int id)
        {
            var ans = _repo.RetrieveAnswerById(id);
            if (ans == null) return NotFound();
            return Ok(ans);
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllAnswer()
        {
            return Ok(_repo.RetrieveAllAnswers());
        }

        [HttpPost]
        public async Task<IActionResult> PostAnswer([FromForm] string prob, string ans, IFormFileCollection probPics,
            IFormFileCollection ansPics, float ionPeak, FormulaConfig formula)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            var ans = await _repo.DeleteAnswerById(id);
            if (ans) return Ok();
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer([FromForm] string prob, string ans, IFormFileCollection proPics,
            IFormFileCollection ansPics, float ionPeak, FormulaConfig formula)
        {
            throw new NotImplementedException();
        }
    }
}