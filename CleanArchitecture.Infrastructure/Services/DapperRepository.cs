using CleanArchitecture.Application.Features.Auth.GetAll;
using CleanArchitecture.Application.Features.Auth.GetById;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Infrastructure.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IDbConnection _connection;

        public DapperRepository(IDbConnection? connection = null)
        {
            _connection = connection;
        }

        #region User

        public async Task<IEnumerable<GetAllQueryResponse>> GetAllWithUserRolesAsync(GetAllQuery request)
        {

            string sql = @"
            SELECT 
            u.Id AS UserId
            , u.*
            , r.Id AS RoleId
            , r.*
            FROM AppUsers u
            LEFT JOIN AppUserRoles ur WITH(NOLOCK) ON ur.UserId = u.Id         
            LEFT JOIN AppRoles r WITH(NOLOCK) ON r.Id = ur.RoleId
            WHERE u.IsDeleted = 0";

            var filterConditions = new List<string>();
            var parameters = new DynamicParameters();

            // Dinamik Filtreleme
            if (request.Filter != null && request.Filter.Any())
            {
                foreach (var filter in request.Filter)
                {
                    if (string.IsNullOrEmpty(filter.Value)) continue;
                    var parameterName = $"@{filter.Field}";
                    if (filter.Field == "userRoles") filter.Field = "r.Description";
                    if (filter.Field == "id") filter.Field = "u.Id";
                    switch (filter.Operator.ToLower())
                    {
                        case "eq":
                            filterConditions.Add($"{filter.Field} = {parameterName}");
                            parameters.Add(parameterName, filter.Value);
                            break;
                        case "neq":
                            filterConditions.Add($"{filter.Field} != {parameterName}");
                            parameters.Add(parameterName, filter.Value);
                            break;
                        case "contains":
                            filterConditions.Add($"{filter.Field} LIKE {parameterName}");
                            parameters.Add(parameterName, $"%{filter.Value}%");
                            break;
                        case "notcontains":
                            filterConditions.Add($"{filter.Field} NOT LIKE {parameterName}");
                            parameters.Add(parameterName, $"%{filter.Value}%");
                            break;
                        case "startswith":
                            filterConditions.Add($"{filter.Field} LIKE {parameterName}");
                            parameters.Add(parameterName, $"{filter.Value}%");
                            break;
                        case "endswith":
                            filterConditions.Add($"{filter.Field} LIKE {parameterName}");
                            parameters.Add(parameterName, $"%{filter.Value}");
                            break;
                        default:
                            throw new NotSupportedException($"Operator '{filter.Operator}' is not supported.");
                    }
                }
            }

            if (filterConditions.Any())
            {
                sql += " AND " + string.Join(" AND ", filterConditions);
            }

            // Dinamik Sıralama
            if (request.Sort != null && !string.IsNullOrEmpty(request.Sort.Field))
            {
                if (request.Sort.Field == "userRoles") request.Sort.Field = "r.Description";
                if (request.Sort.Field == "id") request.Sort.Field = "u.Id";
                var sortDirection = request.Sort.Dir.ToLower() == "desc" ? "DESC" : "ASC";
                sql += $" ORDER BY {request.Sort.Field} {sortDirection}";
            }
            else
            {
                sql += " ORDER BY u.Id ASC"; // Varsayılan sıralama
            }

            // Pagination
            sql += @" OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Skip", (request.PageNumber - 1) * request.PageSize);
            parameters.Add("@PageSize", request.PageSize);

            var userDic = new Dictionary<string, GetAllQueryResponse>();

            await _connection.QueryAsync<GetAllQueryResponse, AppRole, GetAllQueryResponse>(
                sql,
                (user, role) =>
                {
                    GetAllQueryResponse userEntity;
                    if (!userDic.TryGetValue(user.Id, out userEntity))
                    {
                        userDic.Add(user.Id, userEntity = user);
                    }
                    if (role != null)
                    {
                        if (userEntity.Roles == null) userEntity.Roles = new List<AppRole>();
                        if (!userEntity.Roles.Any(x => x.Id == role.Id))
                        {
                            userEntity.Roles.Add(role);
                        }
                    }
                    return userEntity;
                },
                parameters,
                splitOn: "RoleId"
            );

            return userDic.Values;
        }



        public async Task<GetByIdQueryResponse> GetByIdWithUserRolesAsync(string id)
        {
            string sql = @"
                SELECT 
                u.Id AS UserId
				, u.*
                ,r.Id AS RoleId
				, r.*
                FROM AppUsers u
				LEFT JOIN AppUserRoles ur WITH(NOLOCK) ON ur.UserId = u.Id         
				LEFT JOIN AppRoles r WITH(NOLOCK) ON r.Id = ur.RoleId
                WHERE u.IsDeleted=0 AND u.Id = @id";

            var userDic = new Dictionary<string, GetByIdQueryResponse>();

            await _connection.QueryAsync<GetByIdQueryResponse, AppRole, GetByIdQueryResponse>(
                sql,
                (user, role) =>
                {
                    GetByIdQueryResponse userEntity;
                    if (!userDic.TryGetValue(user.Id, out userEntity))
                    {
                        userDic.Add(user.Id, userEntity = user);
                    }
                    if (role != null)
                    {
                        if (userEntity.Roles == null) userEntity.Roles = new List<AppRole>();
                        if (!userEntity.Roles.Any(x => x.Id == userEntity.Id))
                        {
                            userEntity.Roles.Add(role);
                        }
                    }
                    return userEntity;
                }, new { id },
                splitOn: "RoleId"
            );
            return userDic.Values.FirstOrDefault();
        }


        #endregion
    }
}
