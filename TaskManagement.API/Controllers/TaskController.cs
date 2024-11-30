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

        [HttpGet]        
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

        [HttpGet]
        [Route("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDTO taskDTO)
        {       
            var response = await _taskService.CreateTaskAsync(taskDTO, GetUserId());

            return response.Result switch
            {
                ServiceResult.Success => Ok(response),                
                _ => BadRequest(response),
            };
        }

        [HttpPut]
        [Route("{id}")]
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

        [HttpDelete]
        [Route("{id}")]
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
