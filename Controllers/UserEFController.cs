using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;


    public UserEFController(IConfiguration config)
    {
        //  Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {

        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;

    }


    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {

        User? user = _entityFramework.Users
        .Where(u => u.UserId == userId)
        .FirstOrDefault<User>();

        if (user != null)
        {
            return user;
        }

        throw new Exception("Failed to get User");

    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {

        User? userDb = _entityFramework.Users
        .Where(u => u.UserId == user.UserId)
        .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");

        }

        throw new Exception("Failed to Get User");


    }

    [HttpPost("AddUser")]
    public IActionResult Adduser(UserToAddDto user)
    {
        //User userDb = new User();

        User userDb = _mapper.Map<User>(user);

        // userDb.Active = user.Active;
        // userDb.FirstName = user.FirstName;
        // userDb.LastName = user.LastName;
        // userDb.Email = user.Email;
        // userDb.Gender = user.Gender;

        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User");


    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {

        User? userDb = _entityFramework.Users
       .Where(u => u.UserId == userId)
       .FirstOrDefault<User>();

        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User");

        }

        throw new Exception("Failed to Get User");


    }

    [HttpGet("GetUserSalary")]
    public IEnumerable<UserSalary> GetUserSalary()
    {
        IEnumerable<UserSalary> userSalary = _entityFramework.UserSalary.ToList<UserSalary>();
        return userSalary;

    }
    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {

        UserSalary? userSalary = _entityFramework.UserSalary
        .Where(u => u.UserId == userId)
        .FirstOrDefault<UserSalary>();

        if (userSalary != null)
        {
            return userSalary;
        }

        throw new Exception("Failed to get User salary");

    }
    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {

        UserSalary? userSalaryDb = _entityFramework.UserSalary
        .Where(u => u.UserId == userSalary.UserId)
        .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            userSalaryDb.UserId = userSalary.UserId;
            userSalaryDb.Salary = userSalary.Salary;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Salary");

        }

        throw new Exception("Failed to Get User Salary");


    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {

        UserSalary userSalaryDb = _mapper.Map<UserSalary>(userSalary);


        _entityFramework.Add(userSalaryDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User Salary");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {

        UserSalary? userSalaryDb = _entityFramework.UserSalary
       .Where(u => u.UserId == userId)
       .FirstOrDefault<UserSalary>();

        if (userSalaryDb != null)
        {
            _entityFramework.UserSalary.Remove(userSalaryDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");

        }

        throw new Exception("Failed to Get User Salary");


    }

    [HttpGet("GetUserJobInfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> userJobInfo = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return userJobInfo;

    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
        .Where(u => u.UserId == userId)
        .FirstOrDefault<UserJobInfo>();

        if (userJobInfo != null)
        {
            return userJobInfo;
        }

        throw new Exception("Failed to get User Job Info");

    }
    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {

        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
        .Where(u => u.UserId == userJobInfo.UserId)
        .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            userJobInfoDb.UserId = userJobInfo.UserId;
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Job Info");

        }

        throw new Exception("Failed to Get User Job Info");


    }
    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
    {

        UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userJobInfo);


        _entityFramework.Add(userJobInfoDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add New User Job Info");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
       .Where(u => u.UserId == userId)
       .FirstOrDefault<UserJobInfo>();

        if (userJobInfoDb != null)
        {
            _entityFramework.UserJobInfo.Remove(userJobInfoDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Job info");

        }

        throw new Exception("Failed to Get User job info");


    }


}


