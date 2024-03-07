using APIRoulette.DTOs;
using APIRoulette.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {

        private readonly BetDbContext _betDbContext;
        public BetController(BetDbContext betDbContext)

        {
            _betDbContext = betDbContext;
        }
        
        // GET api/<ValuesController>/5
        [HttpGet("Roulette/{id}")]
        public async Task<ActionResult<RouletteGetDTO>> Get(int id)
        {
            try
            {
                var data =await _betDbContext.Roulette.FirstOrDefaultAsync(s=>s.id==id);
                var config = await _betDbContext.ConfigBet.FirstOrDefaultAsync();
                if (data == null) return NotFound("Ruleta no encontrada");

                return new RouletteGetDTO
                {
                    id= data.id,
                    numMax = data.numMax,
                    numMin = data.numMin,
                    minValue = config.valMinBet,
                    maxValue = config.valMaxBet,
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] RouletteDTO data)
        {
            try
            {
                var config = await _betDbContext.ConfigBet.FirstOrDefaultAsync();

                if (data.numMin < config.numMinRoulette) return BadRequest($"El numero minimo aceptado es: {config.numMinRoulette}");
                if (data.numMax > config.numMaxRoulette) return BadRequest($"El numero maximo aceptado es: {config.numMaxRoulette}");

                var roulette = new Roulette()
                {
                    numMax = data.numMax.Value,
                    numMin = data.numMin.Value,
                };
                await _betDbContext.AddAsync(roulette);
                await _betDbContext.SaveChangesAsync();

                return roulette.id;
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NewBet")]
        public async Task<ActionResult<BetDTO>> PostBet([FromBody] BetDTO data)
        {
            try
            {
             
                var config = await _betDbContext.ConfigBet.FirstOrDefaultAsync();
              
                var roulette = await _betDbContext.Roulette.FirstOrDefaultAsync(s => s.id == data.roulette_Id);
                if (roulette==null) return BadRequest("Ruleta no encontrada");

                if (data.betValue < config.valMinBet) return BadRequest($"El valor minimo aceptado para apostar es: {config.valMinBet}");
                if (data.betValue > config.valMaxBet) return BadRequest($"El valor maximo aceptado para apostar es: {config.valMaxBet}");

                if (data.betNumber < roulette.numMin) return BadRequest($"El numero minimo permitido en esta ruleta es: {roulette.numMin}");
                if (data.betNumber > roulette.numMax) return BadRequest($"El numero maximo permitido en esta ruleta es: {roulette.numMax}");
               
                if (data.betNumber == data.numberRandon) data.won = true;

                if (data.numberRandon == null) return Ok("valid");

                var bet = new Bet{
                    betNumber= data.betNumber,
                    betValue = data.betValue,
                    roulette_Id = roulette.id,
                    won= data.won.Value,                 
                };

                await _betDbContext.AddAsync(bet);
                await _betDbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
    }
}
