﻿using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.State;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.GetAll
{
    public class GetAllQuery : IRequest<Result<List<GetAllQueryResponse>>>
    {
        public int PageNumber { get; set; } // Sayfa numarası
        public int PageSize { get; set; } // Sayfa başına kayıt sayısı
        public int Skip { get; set; } // Atlanacak kayıt sayısı
        public StateSort Sort { get; set; } // Sıralama bilgileri
        public List<StateFilter> Filter { get; set; } // Filtre bilgileri
    }
}
