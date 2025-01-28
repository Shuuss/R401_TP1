using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        public List<Devise> devises { get; set; }
        public DevisesController()
        {
            devises = new List<Devise>();

            Devise dollar = new Devise(1, "Dollar", 1.08);
            Devise francSuisse = new Devise(2, "Franc Suisse", 1.07);
            Devise yen = new Devise(3, "Yen", 120);

            devises.Add(dollar);
            devises.Add(francSuisse);
            devises.Add(yen);
        }
        /// <summary>
        /// Displays all currencies.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">When the currencies displays correctly</response>
        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById([FromRoute] int id)
        {
            Devise? devise = devises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        /// <summary>
        /// Add a currency to the currency list.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // POST api/<DevisesController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Replace a currency by another in the list.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete a currency in the list.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}", Name = "GetDevise")]
        public ActionResult<Devise> Delete([FromRoute] int id)
        {
            Devise? devise = devises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return devise;
        }

        
    }
}
