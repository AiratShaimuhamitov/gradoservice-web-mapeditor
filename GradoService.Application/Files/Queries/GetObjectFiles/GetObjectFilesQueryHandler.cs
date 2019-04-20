﻿using AutoMapper;
using GradoService.Application.Files.Queries.Models;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Files.Queries.GetObjectFiles
{
    public class GetObjectFilesQueryHandler : IRequestHandler<GetObjectFilesQuery, IEnumerable<FileInfoDto>>
    {
        private readonly FileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetObjectFilesQueryHandler(FileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileInfoDto>> Handle(GetObjectFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _fileRepository.GetObjectFiles(request.TableId, request.ObjectId);

            var filesDto = _mapper.Map<IEnumerable<FileInfoDto>>(files);

            return filesDto;
        }
    }
}