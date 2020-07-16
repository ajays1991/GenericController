using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AutoMapper;

using TestGenericController.Entities;
using TestGenericController.Controllers;
using Data.Repositories;
using Data.entities;

namespace GenericController.Tests.Controllers
{
    public class RestControllerTests : IDisposable
    {
        private MockRepository _mockRepository;
        private Mock<IGenericRepository<Albums>> _mockGenericRepository;
        private Mock<IMapper> _mockMapper;

        public RestControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockGenericRepository = _mockRepository.Create<IGenericRepository<Albums>>();
            _mockMapper = _mockRepository.Create<IMapper>();
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        private RestController<Albums, AlbumRequest, AlbumResponse> createAlbumController()
        {
            return new RestController<Albums, AlbumRequest, AlbumResponse>(
                _mockGenericRepository.Object,
                _mockMapper.Object
                );
        }

        [Fact]
        public void Verify_Create_Album_To_Pass()
        {
            AlbumRequest request = new AlbumRequest() { Name = "The Wall", Artist = "Pink Floyd" };
            Albums album = new Albums() { Id = 1, Name = "The Wall", Artist = "Pink Floyd" };

            _mockMapper.Setup(i => i.Map<AlbumRequest, Albums>(request)).Returns(album);
            _mockGenericRepository.Setup(i => i.Add(album)).ReturnsAsync(album);
            _mockMapper.Setup(i => i.Map<Albums, AlbumResponse>(album));
            var restController = createAlbumController();
            var result = restController.Create(request);
            Assert.NotNull(result);
            //Assert.IsType<AlbumResponse>(result.Result);
        }

        [Fact]
        public void Verify_Get_Album_To_Pass()
        {
            int key = 1;
            Albums album = new Albums() { Id = 1, Name = "The Wall", Artist = "Pink Floyd" };
            _mockGenericRepository.Setup(i => i.Get(key)).ReturnsAsync(album);
            _mockMapper.Setup(i => i.Map<Albums, AlbumResponse>(album));
            var restController = createAlbumController();
            var result = restController.Get(1);
            Assert.NotNull(result);
        }
    }
}
