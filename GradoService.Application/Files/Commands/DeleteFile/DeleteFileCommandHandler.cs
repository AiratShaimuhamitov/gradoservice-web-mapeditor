using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Files.Commands.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
    {
        private readonly FileRepository fileRepository;

        public DeleteFileCommandHandler(FileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            await fileRepository.DeleteFile(request.TableId, request.FileId);

            return Unit.Value;
        }
    }
}
