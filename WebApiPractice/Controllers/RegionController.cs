using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Data;
using WebApiPractice.Models.Domain;
using WebApiPractice.Models.Dto;

namespace WebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {


        private readonly WebApiPracticeDbContext dbContext;
        public RegionController(WebApiPracticeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //get all regions
        //GET: https://Localhost:portnumber//regions//api
        [HttpGet]
        public IActionResult GetAll() {
            //get data from database and store it as list in variable
            var regionsDomain = dbContext.Region.ToList();

            //map domain model to dtos
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,

                });
             
            }

            //return dtos
            return Ok(regionsDto);
        }

        //get region through id 
        //GET: https://Localhost:portnumber//regions//api//{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id )
        {
            //get data from database through id 
           var regionDomain = dbContext.Region.FirstOrDefault(X=>X.Id==id);
            if(regionDomain==null)
            {
                return NotFound();
            }

            //map the datamodel to dto
            var regionDto = new RegionDto {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            return Ok(regionDto);

         
        }
        //post operation to create new region

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDto addregionDto) {
            var regiondomainmodel = new Region
            {
                //map dto to domain model
                Code = addregionDto.Code,
                Name = addregionDto.Name,
                RegionImageUrl = addregionDto.RegionImageUrl
            };
        //use domain model to crate region
        dbContext .Region.Add(regiondomainmodel);
            dbContext.SaveChanges();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regiondomainmodel.Id,
                Code = regiondomainmodel.Code,
                Name = regiondomainmodel.Name,
                RegionImageUrl = regiondomainmodel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id=regionDto.Id}, regionDto);


        }
        // Update region by id
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            // Get the region from the database by id
            var regionDomain = dbContext.Region.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // Update properties of the region domain model based on the DTO
            regionDomain.Code = updateRegionDto.Code;
            regionDomain.Name = updateRegionDto.Name;
            regionDomain.RegionImageUrl = updateRegionDto.RegionImageUrl;

            // Save changes to the database
            dbContext.SaveChanges();

            // Map the updated region domain model back to DTO
            var updatedRegionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            // Return the updated region DTO
            return Ok(updatedRegionDto);
        }
        // Delete region by id
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            // Get the region from the database by id
            var regionDomain = dbContext.Region.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            // Remove the region from the database
            dbContext.Region.Remove(regionDomain);
            dbContext.SaveChanges();

            // Return a 204 No Content response indicating successful deletion
            return NoContent();
        }

    }
}
