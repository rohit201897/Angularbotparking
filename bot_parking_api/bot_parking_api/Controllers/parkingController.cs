using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using bot_parking_api.Models;
using System.Reflection;

namespace bot_parking_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class parkingController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = new List<Parkinglots>();
        using (var connection = new SqlConnection("Data Source = .; Initial Catalog = bot_parking; Integrated Security = true"))
        {
            connection.Open();
            using (var command = new SqlCommand("SELECT * FROM parking_lots", connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Parkinglots
                        {
                            org_name = reader.GetString(0),
                            org_id = reader.GetInt32(1),
                            parking_add = reader.GetString(2),
                            vid_src = reader.GetString(3)
                        });
                    }
                }
            }
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Parkinglots model)
    {
        using (var connection = new SqlConnection("Data Source = .; Initial Catalog = bot_parking; Integrated Security = true"))
        {
            connection.Open();
            using (var command = new SqlCommand("INSERT INTO parking_lots (org_name, org_id, parking_add, vid_src) VALUES (@value1, @value2, @value3, @value4)", connection))
            {
                command.Parameters.AddWithValue("@value1", model.org_name);
                command.Parameters.AddWithValue("@value2", model.org_id);
                command.Parameters.AddWithValue("@value3", model.parking_add);
                command.Parameters.AddWithValue("@value4", model.vid_src);
                await command.ExecuteNonQueryAsync();
            }
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Parkinglots model)
    {
        using (var connection = new SqlConnection("Data Source = .; Initial Catalog = bot_parking; Integrated Security = true"))
        {
            connection.Open();
            using (var command = new SqlCommand("DELETE FROM parking_lots WHERE org_name = @value0 AND org_id = @value1 AND parking_add = @value2 AND vid_src = @value3", connection))
            {
                command.Parameters.AddWithValue("@value0", model.org_name);
                command.Parameters.AddWithValue("@value1", model.org_id);
                command.Parameters.AddWithValue("@value2", model.parking_add);
                command.Parameters.AddWithValue("@value3", model.vid_src);
                await command.ExecuteNonQueryAsync();
            }
        }
        return Ok();
    }
}


