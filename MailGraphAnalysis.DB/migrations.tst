Add-Migration Initial -Context DataContext

Add-Migration Initial -Context DataContext -OutputDir Data/DataDb
Add-Migration PersistedGrantDbMigration -Context PersistedGrantDbContext -OutputDir Data/PersistedGrantDb
Add-Migration ConfigurationDbMigration -Context ConfigurationDbContext -OutputDir Data/ConfigurationDb

https://randomuser.me/api/?results=10

return Ok() ← HTTP 200
return Created() ← HTTP 201
return NoContent(); ← HTTP 204
return BadRequest(); ← HTTP 400
return Unauthorized(); ← HTTP 401
return NotFound(); ← HTTP 404
