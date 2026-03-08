 
using OpticalFibersRetailShop.Models.DTO;
using OpticalFibersRetailShop.Utilities;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using OpticalFibersRetailShop.Models.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;

namespace OpticalFibersRetailShop.DAL
{
    public class UserRepo : IUserRepo
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public UserRepo(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


//Login check
        public LoginResponse LoginCheck(LoginRequest request)
        {
            LoginResponse lr = new LoginResponse();
            try
            {
                var u = _context.userEntity.Where(a => a.Email == request.Uname).FirstOrDefault();
                //var u = _context.userEntity.Where(a => a.IsDeleted == false && a.IsActive == true && a.Email == request.Uname).FirstOrDefault();

                if (u == null)
                {
                    lr.statusCode = 0;
                    lr.Message = "Please enter valid email";
                    return lr;
                }
                else
                {
                    //var p = EncryptTool.Decrypt(u.PasswordHash);
                    //if (request.Password == EncryptTool.Decrypt(u.PasswordHash))
                    if(u.PasswordHash==request.Password)
                    {
                        lr.statusCode = 1;
                        lr.Message = "success";
                        lr.userTypeName = _context.uTypeEntity.Where(a => a.RoleId == u.RoleId).Select(b => b.RoleName).FirstOrDefault();
                        lr.userName = u.FullName;
                        lr.userId = u.UserId;

                        // entry in lastlogin detials

                        lr.statusCode = 1;
                        lr.Message = "Login success";
                        return lr;
                    }
                    else
                    {
                        lr.statusCode = 0;
                        lr.Message = "Password incorrect";
                        return lr;
                    }
                }

            }
            catch (Exception ex)
            {


            }

            return lr;
        }

        //Count for SuperAdmin Dashboards
        public int TotalSubscriptions()
        {
            int count = _context.userEntity.Where(a => a.IsDeleted == false).Count();
            return count;
        }
        //User Type
        public List<UserTypeDTO> GetUserType(int id)
        {
            var result = (from user in _context.uTypeEntity
                          where user.IsDeleted == false
                          select new UserTypeDTO
                          {
                              RoleId = user.RoleId,
                              RoleName = user.RoleName
                          }).ToList();

            return result;
        }


        public GenericResponse AddUserType(UserTypeDTO obj, int id)
        {
            GenericResponse response = new GenericResponse();
            UserTypeEntites entity = new UserTypeEntites();
            int count = _context.uTypeEntity.Where(a => a.RoleName == obj.RoleName && a.IsDeleted == false).Count();
            try
            {
                if (count == 0)
                {
                    entity.RoleName = obj.RoleName;
                    entity.IsDeleted = false;

                    _context.uTypeEntity.Add(entity);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = entity.RoleId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "User Type Alredy exist";
                }
            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to add UserType" + ex;
            }
            return response;
        }

        public GenericResponse UpdateUserType(UserTypeDTO obj, int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.uTypeEntity.Where(a => a.RoleId == obj.RoleId && a.IsDeleted == false).FirstOrDefault();
            int count = _context.uTypeEntity.Where(a => a.RoleName == obj.RoleName).Count();
            try
            {
                if (count == 1)
                {
                    result.RoleId = obj.RoleId;
                    result.RoleName = obj.RoleName;
                    result.IsDeleted = false;
                    _context.uTypeEntity.Update(result);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = result.RoleId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "User Type Alredy exist";
                }

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to Update UserType" + ex;
            }
            return response;
        }

        public GenericResponse DeleteUserType(int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.uTypeEntity.Where(a => a.RoleId == id && a.IsDeleted == false).FirstOrDefault();
            try
            {
                result.IsDeleted = true;
                _context.uTypeEntity.Update(result);
                _context.SaveChanges();
                response.statuCode = 1;
                response.message = "Success";
                response.currentId = result.RoleId;

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "Failed to Delete UserType" + ex;
            }
            return response;
        }


        //UserMaster
        public List<UserDto> GetUser(int id)
        {
            var result = (from user in _context.userEntity
                          where user.IsDeleted == false 
                          select new UserDto
                          {
                              UserId = user.UserId,
                              FullName = user.FullName,
                              Email = user.Email,
                              UserType = _context.uTypeEntity.Where(a => a.RoleId == user.RoleId && a.IsDeleted == false).Select(a => a.RoleName).FirstOrDefault()

                          }).ToList();

            return result;
        }


        public GenericResponse AddUser(UserDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            UserEntity entity = new UserEntity();
           
            int count = _context.userEntity.Where(a => a.FullName == obj.FullName&&a.StoreName==obj.StoreName && a.IsDeleted == false).Count();
            try
            {
                if (count < 1)
                {

                    entity.FullName = obj.FullName;
                    entity.StoreName = obj.StoreName;
                    entity.Email = obj.Email;
                    entity.RoleId = _context.uTypeEntity.Where(a => a.RoleName == obj.UserType && a.IsDeleted == false).Select(a => a.RoleId).FirstOrDefault();
                    entity.IsDeleted = false;
                    entity.IsActive = true;
                    entity.PasswordHash = EncryptTool.Encrypt(obj.PasswordHash);
                    entity.Address = obj.AddressLine;
                    _context.userEntity.Add(entity);
                    _context.SaveChanges();
                  
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = entity.UserId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "User Name Alredy exist";
                }
            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to add User" + ex;
            }
            return response;
        }

        public GenericResponse UpdateUser(UserDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.userEntity.Where(a => a.UserId == obj.UserId && a.IsDeleted == false).FirstOrDefault();
            int count = _context.userEntity.Where(a => a.FullName == obj.FullName && a.StoreName==obj.StoreName).Count();
            try
            {
                if (count == 1)
                {
                    result.FullName = obj.FullName;
                    result.StoreName = obj.StoreName;
                    result.Email = obj.Email;
                    result.RoleId = _context.uTypeEntity.Where(a => a.RoleName == obj.UserType && a.IsDeleted == false).Select(a => a.RoleId).FirstOrDefault();
                    result.PasswordHash = EncryptTool.Encrypt(obj.PasswordHash);
                    result.IsDeleted = false;
                    result.IsActive = true;
                    _context.userEntity.Update(result);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = result.UserId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "User Name Alredy exist";
                }

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "Failed to Update User" + ex;
            }
            return response;
        }

        public GenericResponse DeleteUser(int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.userEntity.Where(a => a.UserId == id && a.IsDeleted == false).FirstOrDefault();
            try
            {
                result.IsDeleted = true;
                result.IsActive = false;
                _context.userEntity.Update(result);
                _context.SaveChanges();
                response.statuCode = 1;
                response.message = "Success";
                response.currentId = result.UserId;

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "Failed to Delete User" + ex;
            }
            return response;
        }




        ////Duration Entity

        //public List<DurationDto> GetDuration()
        //{
        //    var result = (from user in _context.duration
        //                  where user.IsDeleted == false
        //                  select new DurationDto
        //                  {
        //                      Durationid = user.Durationid,
        //                      DurationType = user.DurationType
        //                  }).ToList();

        //    return result;
        //}


        //public GenericResponse AddDuration(DurationDto obj, int id)
        //{
        //    GenericResponse response = new GenericResponse();
        //    DurationEntity entity = new DurationEntity();
        //    int count = _context.duration.Where(a => a.DurationType == obj.DurationType && a.CreatedBy == id && a.IsDeleted == false).Count();
        //    try
        //    {
        //        if (count == 0)
        //        {
        //            entity.DurationType = obj.DurationType;
        //            entity.IsDeleted = false;
        //            entity.IsActive = true;
        //            entity.CreatedOn = DateTime.Now;
        //            entity.CreatedBy = id;

        //            _context.duration.Add(entity);
        //            _context.SaveChanges();
        //            response.statuCode = 1;
        //            response.message = "Success";
        //            response.currentId = entity.Durationid;
        //        }
        //        else
        //        {
        //            response.statuCode = 0;
        //            response.message = "Duration Alredy exist";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.statuCode = 0;
        //        response.message = "failed to add Duration" + ex;
        //    }
        //    return response;
        //}

        //public GenericResponse UpdateDuration(DurationDto obj, int id)
        //{
        //    GenericResponse response = new GenericResponse();
        //    var result = _context.duration.Where(a => a.Durationid == obj.Durationid && a.IsDeleted == false).FirstOrDefault();
        //    int count = _context.duration.Where(a => a.DurationType == obj.DurationType && a.IsDeleted==false).Count();
        //    try
        //    {
        //        if (count < 1)
        //        {
        //            result.Durationid = obj.Durationid;
        //            result.DurationType = obj.DurationType;
        //            result.IsDeleted = false;
        //            result.IsActive = true;
        //            result.CreatedOn = result.CreatedOn;
        //            result.UpdatedOn = DateTime.Now;
        //            result.UpdatedBy = id;

        //            _context.duration.Update(result);
        //            _context.SaveChanges();
        //            response.statuCode = 1;
        //            response.message = "Success";
        //            response.currentId = result.Durationid;
        //        }
        //        else
        //        {
        //            response.statuCode = 0;
        //            response.message = "Duration Alredy exist";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        response.statuCode = 0;
        //        response.message = "failed to Update Duration" + ex;
        //    }
        //    return response;
        //}

        //public GenericResponse DeleteDuration(int id)
        //{
        //    GenericResponse response = new GenericResponse();
        //    var result = _context.duration.Where(a => a.Durationid == id && a.IsDeleted == false).FirstOrDefault();
        //    try
        //    {
        //        result.IsDeleted = true;
        //        result.IsActive = false;
        //        _context.duration.Update(result);
        //        _context.SaveChanges();
        //        response.statuCode = 1;
        //        response.message = "Success";
        //        response.currentId = result.Durationid;

        //    }
        //    catch (Exception ex)
        //    {
        //        response.statuCode = 0;
        //        response.message = "Failed to Delete Duration" + ex;
        //    }
        //    return response;
        //}


    }
}
