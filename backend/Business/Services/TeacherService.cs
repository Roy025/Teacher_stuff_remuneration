using AutoMapper;
using Business.Specifications.GeneralSpecifications;
using Business.Specifications.TeacherSpecifications;
using Core.DTOs.TeacherDTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Params;
using Core.Utils;

namespace Business.Services;
public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    public TeacherService(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherLoginDto> CreateTeacherAsync(TeacherCreateDto teacher)
    {
        teacher.Password = BCrypt.Net.BCrypt.HashPassword(teacher.Password);
        var teacherEntity = _mapper.Map<Teacher>(teacher);
        _unitOfWork.Repository<Teacher>().Add(teacherEntity);
        var result = await _unitOfWork.Complete();
        if (result <= 0) return null;
        return GetTeacherLoginDto(teacherEntity);
    }

    public async Task<TeacherOwnResponseDto> GetTeacherByIdAsync(Guid id)
    {
        var spec = new TeacherByIdSpec(id);

        var teachers = await _unitOfWork.Repository<Teacher>().ListAllAsyncWithSpec(spec);
        var teacher = teachers.FirstOrDefault();
        if (teacher == null) throw new NotFoundException("Teacher not found");
        return _mapper.Map<TeacherOwnResponseDto>(teacher);
    }

    public async Task<IReadOnlyList<TeacherResponseDto>> GetTeachersAsync(TeacherReqParams teacherParams)
    {
        var spec = new TeacherSpecification(teacherParams);

        var teachers = await _unitOfWork.Repository<Teacher>().ListAllAsyncWithSpec(spec);

        var retTeachers = _mapper.Map<IReadOnlyList<TeacherResponseDto>>(teachers);

        return retTeachers;
    }

    public async Task<TeacherLoginDto> LoginTeacherAsync(TeacherLoginReqDto teacher)
    {
        var spec = new TeacherByEmailSpec(teacher.Email);
        var teacherEntities = await _unitOfWork.Repository<Teacher>().ListAllAsyncWithSpec(spec);
        var teacherEntity = teacherEntities.FirstOrDefault();
        if(teacherEntity == null) throw new NotFoundException("Teacher not found");
        if (!BCrypt.Net.BCrypt.Verify(teacher.Password, teacherEntity.Password)) throw new UnAuthorizedException("Invalid password");
        return GetTeacherLoginDto(teacherEntity);        
    }

    public async Task<TeacherResponseDto> UpdateTeacherAsync(Guid id, TeacherOwnUpdateDto teacher)
    {
        var teacherEntity = await _unitOfWork.Repository<Teacher>().GetByIdAsync(id);

        if (teacherEntity == null) return null;
        
        TeacherUpdateDtoToTeacher(teacher, teacherEntity);
        
        _unitOfWork.Repository<Teacher>().Update(teacherEntity);
        
        var result = await _unitOfWork.Complete();
        
        if (result <= 0) return null;
        
        return _mapper.Map<TeacherResponseDto>(teacherEntity);
    }

    private void TeacherUpdateDtoToTeacher(TeacherOwnUpdateDto teacher, Teacher teacherEntity)
    {
        teacherEntity.Name = !String.IsNullOrEmpty(teacher.Name) ? teacher.Name : teacherEntity.Name;
        teacherEntity.BankAccount = !String.IsNullOrEmpty(teacher.BankAccount) ? teacher.BankAccount : teacherEntity.BankAccount;
        teacherEntity.Designation = !String.IsNullOrEmpty(teacher.Designation) ? teacher.Designation : teacherEntity.Designation;
    }

    private TeacherLoginDto GetTeacherLoginDto(Teacher teacherEntity)
    {
        return new TeacherLoginDto
        {
            Id = teacherEntity.Id,
            Email = teacherEntity.Email,
            Role = teacherEntity.Role,
            Image = teacherEntity.Image,
            Token = _tokenService.CreateToken(teacherEntity),
            DepartmentId = teacherEntity.DepartmentId
        };
    }
}
