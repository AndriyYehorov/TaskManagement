using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Services;

namespace TaskManagement.API.Controllers
{
    [Route("tasks")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;        

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get all tasks.
        /// </summary>        
        /// <returns>Ok if successful, Not found if tasks are not found.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTasksAsync(
            [FromQuery] TaskFilter? filter,
            string? sortColumn, 
            string? sortOrder, 
            int page = 1, 
            int pageSize = 5)
        {         
            var response = await _taskService.ReadAllTasksAsync(
                filter,
                sortColumn, 
                sortOrder, 
                page, 
                pageSize, 
                GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => Ok(response),
                ServiceResult.NotFound => NotFound(response),
                _ => BadRequest(response),
            };
        }

        /// <summary>
        /// Get a task by ID.
        /// </summary>        
        /// <returns>Ok if successful, Not found if the task is not found.</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTaskAsync(string id)
        {
            var response = await _taskService.ReadTaskAsync(id, GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => Ok(response),
                ServiceResult.NotFound => NotFound(response),
                ServiceResult.Forbidden => Forbid(),
                _ => BadRequest(response),
            };
        }

        /// <summary>
        /// Create a task.
        /// </summary>        
        /// <returns>Ok if successful, BadRequest otherwise.</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]       
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDTO taskDTO)
        {       
            var response = await _taskService.CreateTaskAsync(taskDTO, GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => Ok(response),                
                _ => BadRequest(response),
            };
        }

        /// <summary>
        /// Update a task by ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <returns>Ok if successful, NotFound if task not found, BadRequest otherwise.</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTaskAsync(string id, [FromBody] TaskDTO taskDTO)
        {
            var response = await _taskService.UpdateTaskAsync(id, taskDTO, GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => Ok(response),
                ServiceResult.NotFound => NotFound(response),
                ServiceResult.Forbidden => Forbid(),
                _ => BadRequest(response),
            };
        }

        /// <summary>
        /// Delete a task by ID.
        /// </summary>
        /// <param name="id">Task ID.</param>
        /// <returns>NoContent if successful, NotFound if the task is not found, BadRequest otherwise.</returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTaskAsync(string id)
        {
            var response = await _taskService.DeleteTaskAsync(id, GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => NoContent(),
                ServiceResult.NotFound => NotFound(response),
                ServiceResult.Forbidden => Forbid(),
                _ => BadRequest(response),
            };
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
