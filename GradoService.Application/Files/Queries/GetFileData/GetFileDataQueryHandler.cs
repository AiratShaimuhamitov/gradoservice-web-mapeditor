using AutoMapper;
using GradoService.Application.Files.Queries.Models;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Files.Queries.GetFileData
{
    public class GetFileDataQueryHandler : IRequestHandler<GetFileDataQuery, FileDataDto>
    {
        private readonly FileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileDataQueryHandler(FileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(GetFileDataQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetObjectSpecificFile(request.TableId, request.ObjectId, request.FileId);

            var fileDto = _mapper.Map<FileDataDto>(file);

            return fileDto;
        }
    }
}
