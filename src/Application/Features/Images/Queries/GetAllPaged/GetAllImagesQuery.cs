using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Specifications;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
namespace BlazorHero.CleanArchitecture.Application.Features.Images.Queries.GetAllPaged
{
    public class GetAllImagesQuery : IRequest<PaginatedResult<GetAllPagedImagesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllImagesQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    public class GGetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, PaginatedResult<GetAllPagedImagesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GGetAllImagesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedImagesResponse>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Image, GetAllPagedImagesResponse>> expression = e => new GetAllPagedImagesResponse
            {
                Id = e.Id,
                ImageInfo = e.ImageInfo,
            };
            //var ImageFilterSpec = new ImageFilterSpecification(request.SearchString);
            var data = await _unitOfWork.Repository<Image>().Entities
               //.Specify(ImageFilterSpec)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}