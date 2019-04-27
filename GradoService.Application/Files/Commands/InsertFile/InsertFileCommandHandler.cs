using GradoService.Domain.Entities.File;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Files.Commands.InsertFile
{
    public class InsertFileCommandHandler : IRequestHandler<InsertFileCommand, int>
    {
        private readonly FileRepository _fileRepository;

        public InsertFileCommandHandler(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<int> Handle(InsertFileCommand request, CancellationToken cancellationToken)
        {
            var file = new File
            {
                ObjectId = request.ObjectId,
                Name = request.FileName,
                Data = request.Data,
                IsPhoto = IsImage(request.FileName)
            };

            return await _fileRepository.InsertFile(request.TableId, file);
        }

        private bool IsImage(string fileName)
        {
            var mime = MimeTypes.GetMimeType(fileName);

            return mime.Contains("image");
        }
    }
}
