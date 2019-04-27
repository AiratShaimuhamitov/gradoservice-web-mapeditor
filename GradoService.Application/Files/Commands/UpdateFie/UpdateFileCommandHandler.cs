using GradoService.Domain.Entities.File;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Files.Commands.UpdateFie
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, Unit>
    {
        private readonly FileRepository fileRepository;

        public UpdateFileCommandHandler(FileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var file = new File
            {
                Id = request.FileId,
                ObjectId = request.ObjectId,
                Name = request.FileName,
                Data = request.Data,
                IsPhoto = IsImage(request.FileName)
            };

            await fileRepository.UpdateFile(request.TableId, file);

            return Unit.Value;
        }

        private bool IsImage(string fileName)
        {
            var mime = MimeTypes.GetMimeType(fileName);

            return mime.Contains("image");
        }
    }
}
