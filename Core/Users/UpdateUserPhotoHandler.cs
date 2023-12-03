using Core.Services;
using Core.Users.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class UpdateUserPhotoHandler : IRequestHandler<UpdateUserPhotoRequest>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IUserRepository _userRepository;

        public UpdateUserPhotoHandler(IFileStorage fileStorage, IUserRepository userRepository)
        {
            _fileStorage = fileStorage;
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserPhotoRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(o => o.Uuid == request.UserUuid, token);
            
            var fileExt = request.Photo.FileName.Split('.')[1];

            var fileName = $"{Guid.NewGuid().ToString("N")}.{fileExt}";

            await _fileStorage.SavePhoto(request.Photo, fileName);

            user.Photo = fileName;

            await _userRepository.Save(token);
        }
    }
}
